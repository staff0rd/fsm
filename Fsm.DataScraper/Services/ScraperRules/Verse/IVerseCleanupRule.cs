using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    interface IVerseCleanupRule : IScraperRule
    {
        string Clean(string verse);
    }
}
