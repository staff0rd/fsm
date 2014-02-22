using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    class ChapterIsTildeTerminated : ScraperRule, IChapterRolloverRule
    {
        public Chapter GetChapter(Book book, Chapter chapter, string paragraph)
        {
            if (paragraph.Contains("~~~~~~~~~"))
            {
                var newChapter = new Chapter { Number = book.Chapters.Count + 1 };
                book.Chapters.Add(newChapter);
                return newChapter;
            }
            return chapter;
        }
    }
}
