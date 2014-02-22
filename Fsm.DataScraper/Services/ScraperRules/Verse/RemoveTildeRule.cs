using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class RemoveTildeRule : ScraperRule, IVerseCleanupRule
    {
        public string Clean(string verse)
        {
            return verse.Replace("~", String.Empty);
        }
    }
}
