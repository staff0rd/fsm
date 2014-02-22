using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class BookNameReplaceRule : ReplaceTextRule, IBookRule
    {
        public BookNameReplaceRule(string oldText, string newText) : base(oldText, newText) { }
        public void Update(Book book)
        {
            book.Name = Clean(book.Name);
        }
    }
}
