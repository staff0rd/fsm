using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class BookAbbreviationRule : ScraperRule, IBookRule
    {
        readonly string _abbr;
        public BookAbbreviationRule(string abbr)
        {
            _abbr = abbr;
        }

        public void Update(Book book)
        {
            book.Abbreviation = _abbr.Replace(" ", "").ToLower();
        }
    }
}
