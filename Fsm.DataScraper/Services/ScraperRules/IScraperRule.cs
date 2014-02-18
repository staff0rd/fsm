using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public interface IScraperRule
    {
        bool Required(int bookNumber, int? chapterNumber = null, int? verseNumber = null);
    }
}
