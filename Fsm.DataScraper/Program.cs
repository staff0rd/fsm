﻿using CsQuery;
using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;
using Fsm.DataScraper.Services;

namespace Fsm.DataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            new DataScraperService().GetPages(@"\\linuxb0x\store\www.loose-canon.info");

            Console.WriteLine("Finished");
            Console.ReadKey();
        }


    }
}
