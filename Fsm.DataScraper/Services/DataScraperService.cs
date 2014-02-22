using CsQuery;
using Fsm.Common.Services.ConsoleApp;
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
            int? bookNumber = Prompt.GetInt("Book number to parse:");

            var pages = new List<Book>();

            var htmlPages = Directory.GetFiles(pageDirectory).ToList();
            htmlPages.Sort((x, y) => StrCmpLogicalW(x, y));

            List<Abbreviation> abbreviations = ScrapeBooks(pages, htmlPages, bookNumber);
            Console.WriteLine("{0} pages", pages.Count);

            var errors = SetAbbreviations(pages, abbreviations).ToList();
            if (!bookNumber.HasValue)
                errors.ForEach(Console.WriteLine);
                        
            
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
