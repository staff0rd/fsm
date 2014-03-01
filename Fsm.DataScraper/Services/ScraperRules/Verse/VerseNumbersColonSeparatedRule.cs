using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class VerseNumbersColonSeparatedRule : ScraperRule, IVerseMatchRule
    {
        public int[] GetMatches(string paragraph)
        {
            return Regex.Matches(paragraph, @"(\d+):(\d+)").OfType<Match>().Select(p => p.Index).ToArray();
        }
        
        public string CleanVerse(string verse)
        {
            return verse.Substring(verse.IndexOf(' ') + 1);
        }
    }
}
