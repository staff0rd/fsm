using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public abstract class ScraperRule : IScraperRule
    {
        public int BookNumber { get; set; }
        
        public int? ChapterNumber { get; set; }
        
        public int? VerseNumber { get; set; }

        public bool Required(int bookNumber, int? chapterNumber = null, int? verseNumber = null)
        {
            return BookNumber == bookNumber && (!ChapterNumber.HasValue || ChapterNumber.Value == chapterNumber) && (!VerseNumber.HasValue || VerseNumber.Value == verseNumber);
        }
    }
}
