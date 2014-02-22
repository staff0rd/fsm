using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ReplaceTextRule : ScraperRule
    {
        readonly string _oldText, _newText;
        public ReplaceTextRule(string oldText, string newText) : base() {
            _oldText = oldText;
            _newText = newText;
        }
 
        public string Clean(string verse)
        {
            return verse.Replace(_oldText, _newText).Trim();
        }
    }
}
