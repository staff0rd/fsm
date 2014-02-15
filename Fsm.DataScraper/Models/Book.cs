using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fsm.DataScraper.Models
{
    public class Book
    {
        public string FileName { get; set; }
        
        public string Name { get; set; }

        [XmlIgnore]
        public CQ Dom { get; set; }

        public int Number { get; set; }

        public List<Chapter> Chapters { get; set; }

        public string Abbreviation { get; set; }

        public Book()
        {
            Chapters = new List<Chapter>();
        }
    }
}
