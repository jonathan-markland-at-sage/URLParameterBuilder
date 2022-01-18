using System;

namespace ApiQueryParameterBuilder
{
    internal class Query
    {
        private int initialLength;

        internal Query(string pathPart)
        {
            QueryString = pathPart;
            initialLength = pathPart.Length;
        }

        internal void Append(string s)
        {
            QueryString = QueryString + s;
        }

        internal bool HasNoParameters 
        { 
            get { return QueryString.Length == initialLength; } 
        }

        internal string QueryString { get; private set; }
    }
}
