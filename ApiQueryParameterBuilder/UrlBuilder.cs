using System;

namespace ApiQueryParameterBuilder
{
    internal class UrlBuilder
    {
        private int initialLength;

        internal UrlBuilder(string pathPart)
        {
            Url = pathPart;
            initialLength = pathPart.Length;
        }

        /// <summary>
        /// Append a fully-formed parameter part to the request url.
        /// </summary>
        internal void Append(string s)
        {
            Url = Url + s;
        }

        /// <summary>
        /// Returns true if no parameters have been appended onto the initial path.
        /// </summary>
        internal bool HasNoParameters 
        { 
            get { return Url.Length == initialLength; } 
        }

        /// <summary>
        /// Returns the full URL string thus far.
        /// </summary>
        internal string Url { get; private set; }
    }
}
