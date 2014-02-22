using CsQuery;
using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services
{
    public class DataScraperService
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        static extern int StrCmpLogicalW(string psz1, string psz2);

        public List<Book> GetPages(string pageDirectory, string pageName = null, int? bookNumber = null)
        {
            var pages = new List<Book>();

            var htmlPages = Directory.GetFiles(pageDirectory).ToList();
            htmlPages.Sort((x, y) => StrCmpLogicalW(x, y));

            List<Abbreviation> abbreviations = ScrapeBooks(pages, htmlPages, pageName, bookNumber);

            var errors = SetAbbreviations(pages, abbreviations).ToList();
            if (string.IsNullOrEmpty(pageName))
                errors.ForEach(Console.WriteLine);
                        
            Console.WriteLine("{0} pages", pages.Count);
            return pages;
        }

        private IEnumerable<string> SetAbbreviations(List<Book> pages, List<Abbreviation> abbreviations)
        {
            foreach (var abbreviation in abbreviations)
            {
                var book = pages.SingleOrDefault(p => p != null && p.Name == abbreviation.Name);
                if (book == null)
                    yield return string.Format("No match on {0}", abbreviation.Name);
                else
                    book.Abbreviation = abbreviation.Abbr;
            }
        }

        private static List<Abbreviation> ScrapeBooks(List<Book> pages, List<string> htmlPages, string pageName = null, int? bookNumber = null)
        {
            var specificPage = !string.IsNullOrEmpty(pageName) && bookNumber.HasValue;
            List<Abbreviation> abbreviations = null;

            foreach (var htmlPage in htmlPages)
            {
                switch (Path.GetFileName(htmlPage))
                {
                    case ("page3.htm"):
                        {
                            abbreviations = new AbbreviationScraper(htmlPage, pages.Count - 1).GetAbbreviations().ToList();
                            break;
                        }
                    default:
                        {
                            if ( !specificPage || Path.GetFileName(htmlPage) == pageName)
                            {
                                var page = new DefaultScraper(htmlPage, specificPage ? bookNumber.Value -1 : pages.Count - 1, true).Scrape();
                                pages.Add(page);
                            }
                            break;
                        }
                }
            }

            return abbreviations;
        }
    }
}
