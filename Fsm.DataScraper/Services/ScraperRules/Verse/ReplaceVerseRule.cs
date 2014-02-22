using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    class ReplaceVerseRule : ReplaceTextRule, IVerseCleanupRule
    {
        public ReplaceVerseRule(string oldText, string newText) : base(oldText, newText) { }
    }
}
