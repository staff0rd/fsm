using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    class BookNameTerminateRule : TerminateRule, IBookRule
    {
        public BookNameTerminateRule(int terminateAt) : base(terminateAt) { }

        public void Update(Book book)
        {
            book.Name = Clean(book.Name);
        }
    }
}
