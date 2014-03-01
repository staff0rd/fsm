using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    interface ISplitParagraphsRule : IScraperRule
    {
        IEnumerable<string> GetParagraphs(IEnumerable<string> paragraphs);
    }
}
