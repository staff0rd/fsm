using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ParagraphRemoveRule : RemoveTextRule, IParagraphCleanupRule {
        public ParagraphRemoveRule(params string[] oldText) : base(oldText) { }
    }
}
