using System.IO;
using Lucene.Net.Analysis;
using System;

namespace Graxei.Search.Tests.Analyzer
{
    public abstract class AbstractTestAnalyzer : Lucene.Net.Analysis.Analyzer
    {
        protected abstract string[] Tokens { get; }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new InternalTokenStream(Tokens);
        }

        #region Nested type: InternalTokenStream

        private class InternalTokenStream : TokenStream
        {
            private readonly string[] tokens;
            private int position;

            public InternalTokenStream(string[] tokens)
            {
                this.tokens = tokens;
            }

            [Obsolete("Discontinued for Lucene.Net 3.0")]
            public  Token Next()
            {
                return position >= tokens.Length ? null : new Token(tokens[position++], 0, 0);
            }



            protected override void Dispose(bool disposing)
            {
               
            }

            public override bool IncrementToken()
            {
                return position >= tokens.Length;
            }
        }

        #endregion
    }
}