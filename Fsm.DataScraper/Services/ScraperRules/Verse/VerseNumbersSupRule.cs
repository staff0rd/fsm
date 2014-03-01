using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class VerseNumbersSupRule : ScraperRule, IVerseMatchRule
    {
        public int[] GetMatches(string paragraph)
        {
            return Regex.Matches(paragraph, @"<sup>(\d+)</sup>").OfType<Match>().Select(p => p.Index).ToArray();
        }

        public string CleanVerse(string verse)
        {
            return verse.Substring(verse.IndexOf("</sup>") + 6);
        }
    }
}
