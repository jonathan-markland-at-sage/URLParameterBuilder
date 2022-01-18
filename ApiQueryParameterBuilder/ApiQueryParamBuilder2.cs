using System;

namespace ApiQueryParameterBuilder
{
    public class ApiQueryParamBuilder2
    {
        private bool condition;
        private Query query;
        private ApiQueryParamBuilder apiQueryParamBuilder;

        internal ApiQueryParamBuilder2(bool condition, Query query, ApiQueryParamBuilder apiQueryParamBuilder)
        {
            this.condition = condition;
            this.query = query;
            this.apiQueryParamBuilder = apiQueryParamBuilder;
        }

        private string EscapedName(string unescaped)
        {
            return unescaped; // TODO
        }

        private string EscapedValue(string unescaped)
        {
            return unescaped; // TODO
        }

        public ApiQueryParamBuilder Add(string paramName, string value)
        {
            if (condition == true)
            {
                var prefix = (query.QueryString.Length == 0) ? "?" : "&";
                query.QueryString = query.QueryString + prefix + EscapedName(paramName) + "=" + EscapedValue(value);
            }
            return apiQueryParamBuilder;
        }

        public ApiQueryParamBuilder Add(string paramName, Func<string> valueFunction)
        {
            if (condition == true)
            {
                return Add(paramName, valueFunction());
            }
            return apiQueryParamBuilder;
        }
    }
}
