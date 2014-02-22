﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper.Services.ScraperRules
{
    public class RemoveTextRule : ReplaceTextRule, IVerseCleanupRule
    {
        public RemoveTextRule(string oldText) : base(oldText, string.Empty) { }
    }
}