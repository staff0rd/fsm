using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public interface IVerseMatchRule : IScraperRule
    {
        int[] GetMatches(string paragraph);

        string CleanVerse(string verse);
    }
}
