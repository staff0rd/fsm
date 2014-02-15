using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Models
{
    public class DictionaryEntry<K, V>
    {
        public K Key { get; set; }

        public V Value { get; set; }
    }
}
