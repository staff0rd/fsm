using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Models
{
    public class Book
    {
        public string FileName { get; set; }
        
        public string Name { get; set; }

        public CQ Dom { get; set; }

        public int BookNumber { get; set; }

        public Dictionary<int, Dictionary<int, string>> Chapters { get; set; }

        public Book()
        {
            Chapters = new Dictionary<int, Dictionary<int, string>>();
        }
    }
}
