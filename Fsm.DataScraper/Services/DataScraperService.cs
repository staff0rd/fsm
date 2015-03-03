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

        public List<Book> GetPages(string pageDirectory)
        {
            int? bookNumber = Helpers.GetInt("Book number to parse:");

            var pages = new List<Book>();

            var htmlPages = Directory.GetFiles(pageDirectory).ToList();
            htmlPages.Sort((x, y) => StrCmpLogicalW(x, y));

            List<Abbreviation> abbreviations = ScrapeBooks(pages, htmlPages, bookNumber);
            Console.WriteLine("{0} pages", pages.Count);

            var errors = SetAbbreviations(pages, abbreviations).ToList();

            abbreviations.Where(p => !p.Matched).ToList().ForEach(p => Console.WriteLine(p.Name));

            errors.ForEach(Console.WriteLine);
            
            return pages;
        }

        private IEnumerable<string> SetAbbreviations(List<Book> books, List<Abbreviation> abbreviations)
        {
            foreach (var book in books.Where(p => p.Name != "Empty"))
            {
                var abbr = abbreviations.SingleOrDefault(p => p != null && p.Name == book.Name);
                if (abbr == null)
                    yield return string.Format("No match on {0}", book.Name);
                else
                {
                    abbr.Matched = true;
                    book.Abbreviation = abbr.Abbr;
                }
            }
        }

        private static List<Abbreviation> ScrapeBooks(List<Book> books, List<string> htmlPages, int? bookNumber = null)
        {
            List<Abbreviation> abbreviations = null;

            foreach (var htmlPage in htmlPages)
            {
                switch (Path.GetFileName(htmlPage))
                {
                    case ("page3.htm"):
                        {
                            abbreviations = new AbbreviationScraper(htmlPage, books.Count - 1).GetAbbreviations().ToList();
                            break;
                        }
                    default:
                        {
                            if (!bookNumber.HasValue || bookNumber.Value == books.Count)
                                books.Add(new DefaultScraper(htmlPage, books.Count - 1 , bookNumber.HasValue).Scrape());
                            else
                                books.Add(new EmptyScraper(htmlPage, books.Count - 1).Scrape());
                            break;
                        }
                }
            }

            return abbreviations;
        }
    }
}
