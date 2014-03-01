using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class ParagraphSplitBook48Rule : ScraperRule, ISplitParagraphsRule
    {
        public IEnumerable<string> GetParagraphs(IEnumerable<string> paragraphs)
        {
            string result = "";
            foreach (var paragraph in paragraphs)
            {
                result += paragraph;
                if (paragraph.StartsWith("7:7"))
                    continue;

                if (paragraph.Contains("12:") || paragraph.Contains("13"))
                {
                    var matches = new VerseNumbersColonSeparatedRule().GetMatches(paragraph);
                    for (int i = 0; i < matches.Length; i++)
                    {
                        if (i + 1 < matches.Length)
                            yield return paragraph.Substring(matches[i], matches[i + 1] - matches[i]);
                        else
                            yield return paragraph.Substring(matches[i]);
                    }
                }
                else
                    yield return result;
                result = "";
            }
        }
    }
}
