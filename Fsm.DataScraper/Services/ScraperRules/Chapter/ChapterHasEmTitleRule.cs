using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsm.DataScraper.Models;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ChapterHasEmTitleRule : ChapterHasTitleRule
    {
        public ChapterHasEmTitleRule(bool? skipParagraph = null) : base("<em>Chapter", skipParagraph) {}
    }
}
