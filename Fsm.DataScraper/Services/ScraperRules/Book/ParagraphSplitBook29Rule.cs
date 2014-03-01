using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ParagraphSplitBook29Rule : ScraperRule, ISplitParagraphsRule
    {
        public IEnumerable<string> GetParagraphs(IEnumerable<string> paragraphs)
        {
            foreach (var paragraph in paragraphs)
            {
                if (paragraph.Any())
                {
                    var matches = new VerseNumbersPeriodSeparatedRule().GetMatches(paragraph);
                    for (int i = 0; i < matches.Length; i++)
                    {
                        if (i + 1 < matches.Length)
                            yield return paragraph.Substring(matches[i], matches[i + 1] - matches[i]);
                        else
                            yield return paragraph.Substring(matches[i]);
                    }
                }
            }
        }
    }
}
