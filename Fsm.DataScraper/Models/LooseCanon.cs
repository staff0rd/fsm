using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Models
{
    public class LooseCanon
    {
        public DateTime Scraped { get; set; }

        public string Url { get; set; }

        public List<Book> Books { get; set; }
    }
}
