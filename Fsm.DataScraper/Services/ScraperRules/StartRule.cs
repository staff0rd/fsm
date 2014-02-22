using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class StartRule : ScraperRule
    {
        int _startAt;
        
        public StartRule(int startAt) : base() {
            _startAt = startAt;
        }

        public string Clean(string paragraph)
        {
            if (_startAt < paragraph.Length)
                return paragraph.Substring(_startAt).Trim();
            else
                return paragraph;
        }
    }
}
