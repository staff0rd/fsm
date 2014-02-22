using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services
{
    public class EmptyScraper : Scraper
    {
        public EmptyScraper(string htmlPagePath, int bookNumber) : base(htmlPagePath, bookNumber) { }

        public override Book Scrape()
        {
            _book.Abbreviation = _book.Number.ToString("00");
            _book.Name = _book.Number.ToString("Empty");
            return _book;
        }
    }
}
