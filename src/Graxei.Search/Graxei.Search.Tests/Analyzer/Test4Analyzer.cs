using System;
using System.Collections.Generic;
using System.Text;

namespace Graxei.Search.Tests.Analyzer
{
    public class Test4Analyzer : AbstractTestAnalyzer
    {
        private string[] tokens = { "noise", "mouse", "light" };

        protected override string[] Tokens
        {
            get { return tokens; }
        }
    }
}
