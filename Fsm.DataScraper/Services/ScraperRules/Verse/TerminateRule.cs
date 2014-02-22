using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class TerminateRule : ScraperRule, IVerseCleanupRule
    {
        int _terminateAt;
        
        public TerminateRule(int terminateAt) : base() {
            _terminateAt = terminateAt;
        }

        public string Clean(string verse)
        {
            return verse.Substring(0, _terminateAt).Trim();
        }
    }
}
