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
    public class DefaultScraper : Scraper
    {
        public DefaultScraper(string htmlPagePath, int pageNumber) : base(htmlPagePath, pageNumber) { }

        public override Book Scrape()
        {
            _book.Name = _book.Dom["h2"].Text().Trim();

            if (_book.Number > 0 && _book.Number < 50)
            {
                ExtractText(_book);
            }

            Console.WriteLine("{0:00}. {1} {2}", _book.Number, _book.FileName, _book.Name);
            return _book;
        }

        private void ExtractText(Book book)
        {
            var paragraphs = book.Dom["div.entry > p"].Select(p => WebUtility.HtmlDecode(p.InnerText).Replace("\n", "")).Where(p => !string.IsNullOrWhiteSpace(p));

            var chapter = new Chapter { Number = 1 };
            book.Chapters = new List<Chapter> { chapter };

            foreach (var paragraph in paragraphs)
            {
                var matches = Regex.Matches(paragraph, @"\d+").OfType<Match>().Select(p => p.Index).ToArray();
                chapter.Verses.AddRange(GetVerses(paragraph, matches, chapter.Verses.Count));
                if (paragraph.Contains("~~~~~~~~~") || paragraph.Contains("<em>Chapter"))
                {
                    chapter = new Chapter { Number = book.Chapters.Count + 1 };
                    book.Chapters.Add(chapter);
                }
            }

            Console.WriteLine("Found {0} paragraphs", paragraphs.Count());
        }
        
        private List<Verse> GetVerses(string paragraph, int[] intMatches, int verseOffset)
        {
            try
            {
                var verses = new List<Verse>();

                for (int i = 0; i < intMatches.Length; i++)
                {
                    var verseNumber = verses.Count + 1 + verseOffset;
                    Verse verse = null;
                    if (i + 1 < intMatches.Length)
                        verse = new Verse { Number = verseNumber, Text = paragraph.Substring(intMatches[i], intMatches[i + 1] - intMatches[i]).Trim() };
                    else
                        verse = new Verse { Number = verseNumber, Text = paragraph.Substring(intMatches[i]).Trim() };

                    if (verse.Text.StartsWith(verseNumber.ToString()))
                    {
                        verse.Text = verse.Text.Substring(verseNumber.ToString().Length).Trim();
                        verses.Add(verse);
                    }
                    else
                        throw new IndexException(intMatches[i]);
                }
                return verses;

            }
            catch (IndexException e)
            {
                var remainingIndicies = intMatches.Where(p => p != e.Index).ToArray();
                if (remainingIndicies.Length == 0)
                    Console.WriteLine("Problem");
                return GetVerses(paragraph, remainingIndicies, verseOffset);
            }
        }
    }
}
