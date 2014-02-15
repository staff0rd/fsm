using CsQuery;
using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services
{
    public abstract class Scraper
    {
        protected readonly Book _book;

        public Scraper(string htmlPagePath, int pageNumber)
        {
            _book = new Book
            {
                Number = pageNumber + 1,
                FileName = Path.GetFileName(htmlPagePath),
                Dom = CQ.Create(File.ReadAllText(htmlPagePath))
            };
        }

        public abstract Book Scrape();
    }
}
