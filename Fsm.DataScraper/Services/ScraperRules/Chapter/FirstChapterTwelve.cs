using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class FirstChapterTwelve : ScraperRule, IFirstChapterRule
    {
        public Chapter GetChapter()
        {
            return new Chapter { Number = 12 };
        }
    }
}
