using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class VerseOffsetBumpByFive : ScraperRule, IVerseOffsetRule
    {
        public int GetVerseOffset(Chapter chapter)
        {
            return chapter.Verses.Count + 5;
        }
    }
}
