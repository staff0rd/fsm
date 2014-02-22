using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules.Verse
{
    interface IParagraphCleanupRule : IScraperRule
    {
        string Clean(string paragraph);
    }
}
