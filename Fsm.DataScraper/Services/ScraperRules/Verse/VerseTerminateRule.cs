﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    class VerseTerminateRule : TerminateRule, IVerseCleanupRule
    {
        public VerseTerminateRule(int terminateAt) : base(terminateAt) { }
    }
}
