using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class RemoveTextRule : ScraperRule
    {
        readonly string[] _oldText;
        
        public RemoveTextRule(params string[] oldText) 
        {
            _oldText = oldText;
        }

        public string Clean(string paragraph)
        {
            var newParagraph = paragraph;
            foreach (var text in _oldText)
            {
                newParagraph = newParagraph.Replace(text, string.Empty);
            }
            return newParagraph;
        }
    }
}
