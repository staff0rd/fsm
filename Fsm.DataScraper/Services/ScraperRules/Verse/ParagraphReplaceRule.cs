using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ParagraphReplaceRule : ReplaceTextRule, IParagraphCleanupRule
    {
        public ParagraphReplaceRule(string oldText, string newText) : base(oldText, newText) { }
    }
}
