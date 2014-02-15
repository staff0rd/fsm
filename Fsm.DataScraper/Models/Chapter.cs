using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Models
{
    public class Chapter
    {
        public Chapter()
        {
            Verses = new List<Verse>();
        }
        public int Number { get; set; }

        public string Name { get; set; }

        public List<Verse> Verses { get; set; }
    }
}
