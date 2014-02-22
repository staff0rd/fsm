using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ChapterStartRule : ScraperRule, IFirstChapterRule
    {
        readonly int _startNumber;

        public ChapterStartRule(int startNumber)
        {
            _startNumber = startNumber;

        }

        public Chapter GetChapter()
        {
            return new Chapter { Number = _startNumber };
        }
    }
}
