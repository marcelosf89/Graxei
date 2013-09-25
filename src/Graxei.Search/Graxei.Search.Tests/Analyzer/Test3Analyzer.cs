using System;
using System.Collections.Generic;
using System.Text;

namespace Graxei.Search.Tests.Analyzer
{
    public class Test3Analyzer : AbstractTestAnalyzer
    {
        private string[] tokens = { "music", "elephant", "energy" };

        protected override string[] Tokens
        {
            get { return tokens; }
        }
    }
}
