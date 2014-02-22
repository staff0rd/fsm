using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class BookNameRemoveRule : RemoveTextRule, IBookRule
    {
        public BookNameRemoveRule(string oldText) : base(oldText) { }

        public void Update(Book book)
        {
            book.Name = Clean(book.Name);
        }
    }
}
