using System;

namespace ApiQueryParameterBuilder
{
    public class ApiQueryParamBuilder
    {
        private Query _query;

        internal ApiQueryParamBuilder(string pathPart)
        {
            _query = new Query(pathPart);
        }

        public ApiQueryParamBuilder2 If(bool condition)
        {
            return new ApiQueryParamBuilder2(condition, _query, this);
        }

        /// <summary>
        /// Always include this parameter and value.
        /// </summary>
        public ApiQueryParamBuilder With(string paramName, string value)
        {
            return new ApiQueryParamBuilder2(true, _query, this).Add(paramName, value);
        }

        /// <summary>
        /// Include this value-type parameter if it is not null.
        /// </summary>
        public ApiQueryParamBuilder MaybeWith<T>(string paramName, T? nullableValue) where T:struct
        {
            if (nullableValue.HasValue)
            {
                return new ApiQueryParamBuilder2(true, _query, this).Add(paramName, nullableValue.Value.ToString());
            }
            return this;
        }

        /// <summary>
        /// Return the resulting URL path and query parameters as a complete string.
        /// </summary>
        public override string ToString()
        {
            return _query.QueryString;
        }
    }
}
