using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class VerseStartRule : ScraperRule, IVerseOffsetRule
    {
        readonly int _startNumber;

        public VerseStartRule(int startNumber)
        {
            _startNumber = startNumber;
        }

        public int GetVerseOffset(Chapter chapter)
        {
            return chapter.Verses.Count + _startNumber;
        }
    }
}
