using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class VerseRemoveRule : RemoveTextRule, IVerseCleanupRule
    {
        public VerseRemoveRule(string oldText) : base(oldText) { }
    }
}
