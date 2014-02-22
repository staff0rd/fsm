using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ChapterLastVerseRule : ScraperRule, IChapterRolloverRule
    {
        public Chapter GetChapter(Book book, Chapter chapter, string paragraph, out bool skipParagraph)
        {
            var newChapter = new Chapter { Number = chapter.Number + 1 };
            book.Chapters.Add(newChapter);
            skipParagraph = false;
            return newChapter;
        }
    }
}
