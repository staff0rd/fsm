using Fsm.DataScraper.Models;
using Fsm.DataScraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Fsm.Website.Controllers
{
    public class CanonController : ApiController
    {
        private static LooseCanon _canon;

        public static LooseCanon Canon
        {
            get
            {
                _canon = XmlSerializerService.Deserialize<LooseCanon>(@"D:\git\fsm\canon.xml");
                _canon.Books = _canon.Books.Where(p => p != null && p.Number > 0).ToList();

                return _canon;
            }
        }
        
        public LooseCanon Get()
        {
            return Canon;
        }
    }
}
