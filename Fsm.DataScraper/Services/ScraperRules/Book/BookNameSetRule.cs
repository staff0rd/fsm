using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class BookNameSetRule : ScraperRule, IBookRule
    {
        readonly string _name;
        
        public BookNameSetRule(string name)
        {
            _name = name;
        }

        public void Update(Book book)
        {
            book.Name = _name;
        }
    }
}
