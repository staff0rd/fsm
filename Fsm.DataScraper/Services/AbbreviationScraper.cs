using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services
{
    public class AbbreviationScraper : Scraper
    {
        public AbbreviationScraper(string htmlPagePath, int pageNumber) : base(htmlPagePath, pageNumber) { }

        public override Book Scrape()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Abbreviation> GetAbbreviations()
        {
            var data = WebUtility.HtmlDecode(_book.Dom["div.entry"].RenderSelection());
            var lines = data.Split('\n');
            foreach (var line in lines.Where(p => p.Contains("…")))
            {
                var name = line.Substring(0, line.IndexOf("…"));
                var abbreviation = line.Substring(line.LastIndexOf(",") > 0 ? line.LastIndexOf(",") : line.LastIndexOf("…") + 1).Trim('.').Trim(',').Trim().Replace("<br />", "").Replace("</p>", "").Replace(" ","").ToLower();
                //Console.WriteLine("{0}: {1}", abbreviation, name);
                yield return new Abbreviation { Name = name, Abbr = abbreviation };
            }
        }
    }
}
