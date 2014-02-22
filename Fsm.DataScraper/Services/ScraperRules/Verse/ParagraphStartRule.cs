using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ParagraphStartRule : StartRule, IParagraphCleanupRule
    {
        public ParagraphStartRule(int startAt) : base(startAt) { }
    }
}
