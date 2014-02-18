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
            var pages = new List<Book>();

            var htmlPages = Directory.GetFiles(pageDirectory).ToList();
            htmlPages.Sort((x, y) => StrCmpLogicalW(x, y));

            List<Tuple<string, string>> abbreviations = null;

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
                            var page = new DefaultScraper(htmlPage, pages.Count - 1).Scrape();
                            pages.Add(page);
                            break;
                        }
                }
            }

            foreach (var abbreviation in abbreviations)
            {
                var book = pages.SingleOrDefault(p => p != null && p.Name == abbreviation.Item1);
                if (book == null)
                    Console.WriteLine("No match on {0}", abbreviation.Item1);
                else
                    book.Abbreviation = abbreviation.Item2;
            }

            foreach (var book in pages.Where(p => p != null && string.IsNullOrEmpty(p.Abbreviation)))
            {
                //Console.WriteLine(book.Name);
            }

            Console.WriteLine("{0} pages", pages.Count);
            return pages;
        }
    }
}
