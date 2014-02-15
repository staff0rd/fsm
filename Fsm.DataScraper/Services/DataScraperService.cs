using CsQuery;
using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services
{
    public class DataScraperService
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        static extern int StrCmpLogicalW(string psz1, string psz2);

        public List<Book> GetPages(string pageDirectory)
        {
            var pages = new List<Book>();

            var htmlPages = Directory.GetFiles(pageDirectory).ToList();
            htmlPages.Sort((x, y) => StrCmpLogicalW(x, y));

            foreach (var htmlPage in htmlPages)
            {
                var page = GetPage(htmlPage, pages.Count - 1);
                pages.Add(page);
                Console.WriteLine("{0:00}. {1} {2}", page.BookNumber, page.FileName, page.Name);
            }

            Console.WriteLine("{0} pages", pages.Count);
            return pages;
        }

        private static Book GetPage(string htmlPagePath, int pageNumber)
        {
            var page = new Book
            {
                BookNumber = pageNumber + 1,
                FileName = Path.GetFileName(htmlPagePath),
                Dom = CQ.Create(File.ReadAllText(htmlPagePath))
            };
            page.Name = page.Dom["h2"].Text().Trim();

            if (page.BookNumber > 0 && page.BookNumber < 50)
            {
                ExtractText(page);
            }

            return page;
        }

        private static void ExtractText(Book book)
        {
            var paragraphs = book.Dom["div.entry > p"].Select(p => WebUtility.HtmlDecode(p.InnerText).Replace("\n", "")).Where(p => !string.IsNullOrWhiteSpace(p));

            var verses = new Dictionary<int, string>();
            foreach (var paragraph in paragraphs)
            {
                var matches = Regex.Matches(paragraph, @"\d+").OfType<Match>().Select(p => p.Index).ToArray();
                if (paragraph.Contains("~~~~~~~~~") || paragraph.Contains("<em>Chapter"))
                {
                    AddChapter(book, verses);
                    verses = new Dictionary<int, string>();
                }
                AddVerses(verses, paragraph, matches);
            }

            if (verses.Any())
                AddChapter(book, verses);


            Console.WriteLine("Found {0} paragraphs", paragraphs.Count());
        }

        private static void AddChapter(Book book, Dictionary<int, string> verses)
        {
            book.Chapters[book.Chapters.Count + 1] = verses;
        }

        private static void AddVerses(Dictionary<int, string> allVerses, string paragraph, int[] matches)
        {
            try
            {
                var verses = new Dictionary<int, string>(allVerses);

                for (int i = 0; i < matches.Length; i++)
                {
                    var verseNumber = verses.Count + 1;
                    if (i + 1 < matches.Length)
                        verses.Add(verseNumber, paragraph.Substring(matches[i], matches[i + 1] - matches[i]).Trim());
                    else
                        verses.Add(verseNumber, paragraph.Substring(matches[i]).Trim());

                    if (verses[verseNumber].StartsWith(verseNumber.ToString()))
                        verses[verseNumber] = verses[verseNumber].Substring(verseNumber.ToString().Length).Trim();
                    else
                        throw new IndexException(matches[i]);
                }

                foreach (var verse in verses)
                    allVerses[verse.Key] = verse.Value;
            }
            catch (IndexException e)
            {
                AddVerses(allVerses, paragraph, matches.Where(p => p != e.Index).ToArray());
            }
        }
    }
}
