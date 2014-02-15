using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper
{
    class IndexException : Exception
    {
        public IndexException(int index) : base() {
            Index = index;
        }

        public int Index { get; set; }
    }
}
