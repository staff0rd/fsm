using CsQuery;
using Fsm.DataScraper.Models;
using Fsm.DataScraper.Services.ScraperRules;
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
        List<ScraperRule> _rules = new List<ScraperRule>
        {
            new VerseOffsetBumpByFive { BookNumber = 5, ChapterNumber = 12 },
            new FirstChapterTwelve { BookNumber = 5 },
            new ChapterIsTildeTerminated { BookNumber = 3 }
        };

        public DefaultScraper(string htmlPagePath, int pageNumber) : base(htmlPagePath, pageNumber) { }

        public override Book Scrape()
        {
            if (_book.Number > 5)
                return null;

            DetermineBookName();

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

            var chapter = DetermineFirstChapter(book);
            
            book.Chapters = new List<Chapter> { chapter };

            foreach (var paragraph in paragraphs)
            {
                var matches = Regex.Matches(paragraph, @"\d+").OfType<Match>().Select(p => p.Index).ToArray();

                var verseOffset = DetermineVerseOffset(book, chapter);

                var versesToAdd = GetVerses(paragraph, matches, verseOffset);
                if (versesToAdd.Any())
                    chapter.Verses.AddRange(versesToAdd);
                else if (chapter.Verses.Any())
                    chapter.Verses.Last().Text += " " + paragraph;
                chapter = DetectChapterRollover(book, chapter, paragraph);
            }

            Console.WriteLine("Found {0} paragraphs", paragraphs.Count());
        }

        private void DetermineBookName()
        {
            _book.Name = _book.Dom["h2"].Text().Trim();

            var rule = _rules.OfType<IBookNameRule>().SingleOrDefault(p => p.Required(_book.Number));
            if (rule != null)
                rule.SetBookName(_book);
        }

        private Chapter DetermineFirstChapter(Book book)
        {
            var rule = _rules.OfType<IFirstChapterRule>().SingleOrDefault(p => p.Required(book.Number));
            if (rule != null)
                return rule.GetChapter();
            return new Chapter { Number = 1 };
        }

        private int DetermineVerseOffset(Book book, Chapter chapter)
        {
            var rule = _rules.OfType<IVerseOffsetRule>().SingleOrDefault(p => p.Required(book.Number, chapter.Number));
            if (rule != null)
                return rule.GetVerseOffset(chapter);
            return chapter.Verses.Count;
        }

        private Chapter DetectChapterRollover(Book book, Chapter chapter, string paragraph)
        {
            var rule = _rules.OfType<IChapterRolloverRule>().SingleOrDefault(p => p.Required(book.Number, chapter.Number));
            if (rule != null)
                return rule.GetChapter(book, chapter, paragraph);
            return chapter;
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
                        verse.Text = verse.Text.Substring(verseNumber.ToString().Length).Replace("~", "").Trim();
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
