﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    interface IParagraphCleanupRule : IScraperRule
    {
        string Clean(string paragraph);
    }
}
