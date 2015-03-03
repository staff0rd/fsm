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
            new VerseRemoveRule("<br />"),

            new TerminateRule(181)  {BookNumber = 1, ChapterNumber = 1, VerseNumber = 8 },

            new TerminateRule(6) { BookNumber = 2, ChapterNumber = 1, VerseNumber = 23 },
            new ReplaceTextRule(",",".") { BookNumber = 2, ChapterNumber = 1, VerseNumber = 23 },

            new ChapterHasStrongTitleRule { BookNumber = 3 },
            new VerseRemoveRule("~") { BookNumber = 3, ChapterNumber = 3, VerseNumber = 48 },
            new VerseRemoveRule("~") { BookNumber = 3, ChapterNumber = 4, VerseNumber = 57 },
            new VerseRemoveRule("Here endeth the second book.") { BookNumber =3, ChapterNumber = 2, VerseNumber = 46},
            new VerseRemoveRule("Here Endeth the Third Book") { BookNumber =3, ChapterNumber = 3, VerseNumber = 48},
            new VerseRemoveRule("Here endeth the Fourth Book") { BookNumber =3, ChapterNumber = 4, VerseNumber = 57},
            new ReplaceTextRule(" RAmen", ".  RAmen.") { BookNumber = 3, ChapterNumber = 5, VerseNumber= 44 },

            new BookNameTerminateRule(35) { BookNumber = 5},
            new VerseStartRule(5) { BookNumber = 5, ChapterNumber = 12 },
            new ChapterStartRule(12) { BookNumber = 5 },
            new ChapterLastVerseRule { BookNumber = 5, ChapterNumber = 12, VerseNumber = 15 },

            new ChapterHasEmTitleRule { BookNumber = 6 },

            new ChapterHasStrongTitleRule { BookNumber = 7 },
            
            new ChapterHasStrongTitleRule { BookNumber = 8 },

            new ChapterHasStrongTitleRule { BookNumber = 9 },
            new ParagraphReplaceRule("32 The third", "43 The third") { BookNumber = 9, ChapterNumber = 2, VerseNumber = 23 },
            new ParagraphReplaceRule("33 Last but", "44 Last but") { BookNumber = 9, ChapterNumber = 2, VerseNumber = 23 },

            new VerseNumbersColonSeparatedRule() { BookNumber = 10 },
            new ChapterLastVerseRule() { BookNumber = 10, ChapterNumber = 1, VerseNumber = 90 },
            new ChapterLastVerseRule() { BookNumber = 10, ChapterNumber = 2, VerseNumber = 158 },
            new ChapterLastVerseRule() { BookNumber = 10, ChapterNumber = 3, VerseNumber = 49 },

            new ChapterHasStrongTitleRule(false) { BookNumber = 11 },
            new ParagraphStartRule(27) { BookNumber = 11, ChapterNumber = 1, VerseNumber = 0 },

            new ChapterHasStrongTitleRule { BookNumber = 12 },

            new BookNameReplaceRule("Numberof", "Number of") { BookNumber = 14 },
            new BookNameRemoveRule(":") { BookNumber = 14 },
            new VerseTerminateRule(72) { BookNumber = 14, ChapterNumber = 1, VerseNumber = 16 },

            new ChapterHasStrongTitleRule(false) { BookNumber = 15 },
            new ParagraphStartRule(27) { BookNumber = 15, ChapterNumber = 1, VerseNumber = 0 },

            new BookNameRemoveRule("as passed to Solipsy") { BookNumber = 16 },
            new BookAbbreviationRule("Pasta") { BookNumber = 16 },

            new ChapterHasTitleRule("<strong>P") { BookNumber = 19 },
            new ParagraphReplaceRule("<br />", " ") { BookNumber = 19},
            new VerseRemoveRule("<strong>  </strong>") { BookNumber = 19},
            new ParagraphReplaceRule("32. And I shall ponder", "33. And I shall ponder") { BookNumber = 19, ChapterNumber = 9 },

            new ChapterLastVerseRule { BookNumber = 20, ChapterNumber = 1, VerseNumber = 53 },
            new ChapterLastVerseRule { BookNumber = 20, ChapterNumber = 2, VerseNumber = 23 },
            new ChapterLastVerseRule { BookNumber = 20, ChapterNumber = 3, VerseNumber = 80 },
            new VerseTerminateRule(68) { BookNumber = 20, ChapterNumber = 3, VerseNumber = 80 },
            new ChapterLastVerseRule { BookNumber = 20, ChapterNumber = 4, VerseNumber = 13 },
            new VerseTerminateRule(76) { BookNumber = 20, ChapterNumber = 4, VerseNumber = 13 },
            new VerseReplaceRule(",", ", ") { BookNumber = 20, ChapterNumber = 4 },
            new VerseReplaceRule(",  ", ", ") { BookNumber = 20, ChapterNumber = 4 },
            new VerseTerminateRule(209) { BookNumber = 20, ChapterNumber = 5, VerseNumber = 15 },

            new ChapterHasStrongTitleRule { BookNumber = 21 },
            new ParagraphReplaceRule("8. was", "7. was") { BookNumber = 21, ChapterNumber = 5 },

            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 1, VerseNumber = 24 },
            new VerseTerminateRule(306) { BookNumber = 22, ChapterNumber = 1, VerseNumber = 24 },
            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 2, VerseNumber = 9 },
            new VerseTerminateRule(284) { BookNumber = 22, ChapterNumber = 2, VerseNumber = 9 },
            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 3, VerseNumber = 17 },
            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 4, VerseNumber = 6 },
            new VerseTerminateRule(150) { BookNumber = 22, ChapterNumber = 4, VerseNumber = 6 },
            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 5, VerseNumber = 3 },
            new VerseTerminateRule(257) { BookNumber = 22, ChapterNumber = 5, VerseNumber = 3 },
            new ChapterLastVerseRule { BookNumber = 22, ChapterNumber = 6, VerseNumber = 4 },

            new ChapterLastVerseRule { BookNumber = 23, ChapterNumber = 1, VerseNumber = 62 },
            new VerseNumbersSupRule { BookNumber = 23 },

            new ChapterHasStrongTitleRule { BookNumber = 24 },

            new BookNameRemoveRule(".") { BookNumber = 25 },

            new ChapterHasStrongTitleRule { BookNumber = 26 },

            new ParagraphRemoveRule("<em>", "</em>") { BookNumber = 27 },
            new BookAbbreviationRule("Sol") { BookNumber = 27 },

            new ChapterLastVerseRule { BookNumber = 28, ChapterNumber = 1, VerseNumber = 4},
            new ChapterLastVerseRule { BookNumber = 28, ChapterNumber = 2, VerseNumber = 19},
            new VerseTerminateRule(33) { BookNumber = 28, ChapterNumber = 3, VerseNumber = 15 },

            new BookNameSetRule("Revelations of St. Jason") { BookNumber = 29 },
            new VerseNumbersPeriodSeparatedRule { BookNumber = 29 },
            new ChapterLastVerseRule { BookNumber = 29, ChapterNumber = 1, VerseNumber = 9 },
            new ChapterLastVerseRule { BookNumber = 29, ChapterNumber = 2, VerseNumber = 5 },
            new ChapterLastVerseRule { BookNumber = 29, ChapterNumber = 3, VerseNumber = 3 },
            new ParagraphSplitBook29Rule { BookNumber = 29 },

            new ChapterHasStrongTitleRule { BookNumber = 30 },
            
            new ChapterHasStrongTitleRule { BookNumber = 31 },
            new VerseTerminateRule(35) { BookNumber = 31, ChapterNumber = 1, VerseNumber = 7 },
            new VerseTerminateRule(78) { BookNumber = 31, ChapterNumber = 2, VerseNumber = 6 },

            new VerseTerminateRule(113) { BookNumber = 32, ChapterNumber = 1, VerseNumber = 31 },
            new VerseTerminateRule(35) { BookNumber = 32, ChapterNumber = 1, VerseNumber = 39 },

            new VerseTerminateRule(28) { BookNumber = 34, ChapterNumber = 1, VerseNumber = 59 },
            new VerseTerminateRule(5) { BookNumber = 35, ChapterNumber = 1, VerseNumber = 56 },

            new BookAbbreviationRule("Bobby") {BookNumber = 32 },
            
            new BookAbbreviationRule("Mu 1") {BookNumber = 34 },

            new BookAbbreviationRule("Mu 2") {BookNumber = 35 },

            new ChapterLastVerseRule { BookNumber = 36, ChapterNumber = 1, VerseNumber = 7},
            new VerseReplaceRule("</strong>", "") { BookNumber = 36, ChapterNumber = 1, VerseNumber = 1 },
            new VerseReplaceRule("1 ", "") { BookNumber = 36, ChapterNumber = 1, VerseNumber = 1 },
            new ChapterLastVerseRule { BookNumber = 36, ChapterNumber = 2, VerseNumber = 5},
            new ChapterLastVerseRule { BookNumber = 36, ChapterNumber = 3, VerseNumber = 15},
            new VerseTerminateRule(231) { BookNumber = 36, ChapterNumber = 4, VerseNumber = 9 },

            new BookNameReplaceRule("From", "from") { BookNumber = 37 },
            
            new BookNameReplaceRule("From", "from") { BookNumber = 38 },
            
            new BookNameReplaceRule("From", "from") { BookNumber = 39 },
            
            new BookNameReplaceRule("From", "from") { BookNumber = 40 },
            
            new BookNameReplaceRule("From", "from") { BookNumber = 41 },

            new ChapterLastVerseRule { BookNumber = 42, ChapterNumber = 1, VerseNumber = 6 },
            new ChapterLastVerseRule { BookNumber = 42, ChapterNumber = 2, VerseNumber = 16 },
            new ChapterLastVerseRule { BookNumber = 42, ChapterNumber = 3, VerseNumber = 11 },
            new VerseTerminateRule(28) { BookNumber = 42, ChapterNumber = 4, VerseNumber = 2 },
            
            new ChapterHasStrongTitleRule { BookNumber = 43 },
            new BookAbbreviationRule("Hill") {BookNumber = 43 },
            
            new ChapterHasStrongTitleRule { BookNumber = 45 },
            new BookAbbreviationRule("CCC") {BookNumber = 45 },
            new VerseTerminateRule(134) { BookNumber = 45, ChapterNumber = 2, VerseNumber = 13 },

            new BookNameRemoveRule(".") { BookNumber = 47 },
            new VerseNumbersColonSeparatedRule { BookNumber = 47 },
            new ChapterLastVerseRule { BookNumber = 47, ChapterNumber = 1, VerseNumber = 4 },
            new ChapterLastVerseRule { BookNumber = 47, ChapterNumber = 2, VerseNumber = 5 },
            new ChapterLastVerseRule { BookNumber = 47, ChapterNumber = 3, VerseNumber = 8 },
            new ChapterLastVerseRule { BookNumber = 47, ChapterNumber = 4, VerseNumber = 6 },
            
            new BookNameSetRule("Revelations 1: The Book of Revealed Crapola") { BookNumber = 48 },
            new VerseNumbersColonSeparatedRule { BookNumber = 48 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 1, VerseNumber = 6 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 2, VerseNumber = 4 },
            new VerseRemoveRule("<em>") { BookNumber = 48, ChapterNumber = 2 },
            new VerseRemoveRule("</em>") { BookNumber = 48, ChapterNumber = 2 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 3, VerseNumber = 7 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 4, VerseNumber = 8 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 5, VerseNumber = 21 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 6, VerseNumber = 10 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 7, VerseNumber = 7 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 8, VerseNumber = 22 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 9, VerseNumber = 40 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 10, VerseNumber = 34 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 11, VerseNumber = 49 },
            new ChapterLastVerseRule { BookNumber = 48, ChapterNumber = 12, VerseNumber = 50 },
            new ParagraphRemoveRule("<strong>", "</strong>", "<em>", "</em>") { BookNumber = 48 },
            new ParagraphReplaceRule("20. Bu", "12:20. Bu") { BookNumber = 48, ChapterNumber = 12 },
            new ParagraphSplitBook48Rule { BookNumber = 48 },

            new BookNameRemoveRule(".") { BookNumber = 49 },
            new ParagraphReplaceRule("; and the Flying", "; 9. and the Flying") { BookNumber = 49, ChapterNumber = 1, VerseNumber = 7 },

            new ChapterHasTitleRule("Chapter", false) { BookNumber = 50 },
            new BookAbbreviationRule("AAS") { BookNumber = 50 },
            new VerseRemoveRule("1 ") { BookNumber = 50, ChapterNumber = 1, VerseNumber = 1 },
            
            
        };

        public DefaultScraper(string htmlPagePath, int pageNumber, bool _interactive) : base(htmlPagePath, pageNumber, _interactive) { }

        public override Book Scrape()
        {
            DetermineBookName();

            if (_book.Number > 0 && _book.Number < 51)
            {
                ExtractText(_book);
            }

            Console.WriteLine("{0:00}. {1} {2}", _book.Number, _book.FileName, _book.Name);
            return _book;
        }

        private void ExtractText(Book book)
        {
            var paragraphs = GetParagraphs(book);

            var chapter = DetermineFirstChapter(book);

            book.Chapters = new List<Chapter> { chapter };

            foreach (var paragraph in paragraphs)
            {
                bool skipParagraph;
                chapter = DetectChapterRollover(book, chapter, paragraph, out skipParagraph);
                if (skipParagraph)
                    continue;

                var processed = PreProcessParagraph(book, chapter, paragraph);

                ProcessParagraph(book, chapter, processed);
            }

            if (!book.Chapters.Last().Verses.Any())
                book.Chapters.Remove(book.Chapters.Last());

            ReviewChapters(book, paragraphs);
        }

        private IEnumerable<string> GetParagraphs(Book book)
        {
            var paragraphs = book.Dom["div.entry > p"].Select(p => WebUtility.HtmlDecode(p.InnerHTML).Replace("\n", "")).Where(p => !string.IsNullOrWhiteSpace(p));

            var rule = _rules.OfType<ISplitParagraphsRule>().SingleOrDefault(p => p.Required(book.Number));
            if (rule != null)
                return rule.GetParagraphs(paragraphs);

            return paragraphs;
        }

        private string PreProcessParagraph(Book book, Chapter chapter, string paragraph)
        {
            var result = paragraph;

            foreach(var rule in _rules.OfType<IParagraphCleanupRule>().Where(p => p.Required(book.Number, chapter.Number, chapter.Verses.Count)))
                result = rule.Clean(result);

            return result;
        }

        private void ProcessParagraph(Book book, Chapter chapter, string paragraph)
        {
            var matches = GetMatches(book.Number, chapter.Number, paragraph);

            var verseOffset = DetermineVerseOffset(book, chapter);

            var versesToAdd = GetVerses(book.Number, chapter.Number, paragraph, matches, verseOffset);
            if (versesToAdd.Any())
                chapter.Verses.AddRange(versesToAdd);
            else if (chapter.Verses.Any())
                chapter.Verses.Last().Text += " " + paragraph;
        }

        private int[] GetMatches(int bookNumber, int chapterNumber, string paragraph)
        {

            var rule = _rules.OfType<IVerseMatchRule>().SingleOrDefault(p => p.Required(bookNumber, chapterNumber));
            if (rule != null)
                return rule.GetMatches(paragraph);
            
            return Regex.Matches(paragraph, @"\d+").OfType<Match>().Select(p => p.Index).ToArray();
        }

        private void ReviewChapters(Book book, IEnumerable<string> paragraphs)
        {
            foreach (var chapter in book.Chapters)
            {
                VerseCleanup(book.Number, chapter);

                if (_interactive)
                    ShowEndOfChapter(chapter);

                Console.WriteLine("Found {0} paragraphs", paragraphs.Count());
            }
        }

        private void DetermineBookName()
        {
            _book.Name = _book.Dom["h2"].Text().Trim();

            foreach (var rule in _rules.OfType<IBookRule>().Where(p => p.Required(_book.Number)))
                rule.Update(_book);
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

        private Chapter DetectChapterRollover(Book book, Chapter chapter, string paragraph, out bool skipParagraph)
        {
            var newChapter = chapter;
            var rule = _rules.OfType<IChapterRolloverRule>().SingleOrDefault(p => p.Required(book.Number, chapter.Number, chapter.Verses.Any() ? chapter.Verses.Last().Number : (int?)null));
            if (rule != null)
                return newChapter = rule.GetChapter(book, chapter, paragraph, out skipParagraph);
            skipParagraph = false;
            return newChapter;
        }

        private void VerseCleanup(int bookNumber, Chapter chapter)
        {
            for (int i = 0; i < chapter.Verses.Count; i++)
            {
                foreach (var rule in _rules.OfType<IVerseCleanupRule>().Where(p => p.Required(bookNumber, chapter.Number, chapter.Verses.ElementAt(i).Number)))
                    chapter.Verses.ElementAt(i).Text = rule.Clean(chapter.Verses.ElementAt(i).Text);
            }
        }

        private void ShowEndOfChapter(Chapter chapter)
        {
            Print(string.Format("Chapter {0:00} ", chapter.Number).PadRight(79, '*'));
            if (chapter.Verses.Count > 3)
                PrintVerses(chapter.Verses.Skip(chapter.Verses.Count - 3));
            else
                PrintVerses(chapter.Verses);

            WhatNext(chapter);
        }

        private void WhatNext(Chapter chapter)
        {
            var key = Helpers.GetKey("Show (E)nd, show (A)ll, or press any key to continue:");
            switch (key)
            {
                case ConsoleKey.E: ShowEndOfChapter(chapter); break;
                case ConsoleKey.A: ShowChapter(chapter); break;
            }
        }

        private void ShowChapter(Chapter chapter)
        {
            Print(string.Format("Chapter {0:00} ", chapter.Number).PadRight(79, '*'));
            PrintVerses(chapter.Verses);
            WhatNext(chapter);
        }

        private void Print(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine();
        }

        private void PrintVerses(IEnumerable<Verse> verses)
        {
            foreach (var verse in verses)
                Print("{0:00}. {1}", verse.Number, verse.Text);
        }
        
        private List<Verse> GetVerses(int bookNumber, int chapterNumber, string paragraph, int[] intMatches, int verseOffset)
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

                    var rule = _rules.OfType<IVerseMatchRule>().SingleOrDefault(p => p.Required(bookNumber, chapterNumber));
                    if (rule != null)
                    {
                        verse.Text = rule.CleanVerse(verse.Text);
                        verses.Add(verse);
                    }
                    else if (verse.Text.StartsWith(verseNumber.ToString()))
                    {
                        var length = verseNumber.ToString().Length;
                        if (verse.Text.StartsWith(string.Format("{0}.", verseNumber)))
                            length++;
                        verse.Text = verse.Text.Substring(length).Replace("~", "").Trim();
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
                return GetVerses(bookNumber, chapterNumber, paragraph, remainingIndicies, verseOffset);
            }
        }
    }
}
