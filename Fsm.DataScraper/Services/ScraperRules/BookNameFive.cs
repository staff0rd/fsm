using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsm.DataScraper.Models;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class BookNameFive : ScraperRule, IBookNameRule
    {
        public void SetBookName(Book book)
        {
            book.Name = book.Name.Replace("Chapter 12 through 13", "").Trim();
        }
    }
}
