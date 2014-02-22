using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fsm.DataScraper.Models
{
    public class Verse
    {
        [XmlAttribute]
        public int Number { get; set; }

        public string Text { get; set; }
    }
}
