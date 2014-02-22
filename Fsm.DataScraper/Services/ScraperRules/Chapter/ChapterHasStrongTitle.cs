using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    class ChapterHasStrongTitle : ScraperRule, IChapterRolloverRule
    {
        public Chapter GetChapter(Book book, Chapter chapter, string paragraph, out bool skipParagraph)
        {
            if (paragraph.Contains("<strong>Chapter") && chapter.Verses.Any())
            {
                var newChapter = new Chapter { Number = book.Chapters.Count + 1 };
                book.Chapters.Add(newChapter);
                skipParagraph = true;
                return newChapter;
            }
            skipParagraph = false;
            return chapter;
        }
    }
}
