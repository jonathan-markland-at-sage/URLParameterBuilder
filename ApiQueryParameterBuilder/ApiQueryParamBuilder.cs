using System;
using static ApiQueryParameterBuilder.ApiQueryEscaping;

namespace ApiQueryParameterBuilder
{
    public class ApiQueryParamBuilder
    {
        private UrlBuilder url;

        internal ApiQueryParamBuilder(string pathPart)
        {
            this.url = new UrlBuilder(pathPart);
        }

        /// <summary>
        /// Always include this parameter and string value.  If the string is null an exception is raised.
        /// </summary>
        public ApiQueryParamBuilder With(string paramName, string stringValue)
        {
            if (stringValue != null)
            {
                return Append(paramName, stringValue);
            }

            // It is assumed the caller doesn't know what he's doing.
            // Being too lenient towards nulls masks programming errors.
            throw new ArgumentNullException(nameof(stringValue));
        }

        /// <summary>
        /// Always include this parameter and value converted with ToString().
        /// </summary>
        public ApiQueryParamBuilder With<T>(string paramName, T value) where T : struct
        {
            return With(paramName, value.ToString());
        }

        /// <summary>
        /// Include this value-type parameter if it is not null.
        /// </summary>
        public ApiQueryParamBuilder MaybeWith<T>(string paramName, T? nullableValue) where T:struct
        {
            if (nullableValue.HasValue)
            {
                return With(paramName, nullableValue.Value);
            }
            return this;
        }

        /// <summary>
        /// Include this parameter object, converted with ToString().  If the object is null an exception is raised.
        /// </summary>
        public ApiQueryParamBuilder WithObject(string paramName, object obj) // TODO: test
        {
            if (obj != null)
            {
                return With(paramName, obj.ToString());
            }

            // It is assumed the caller doesn't know what he's doing.
            // Being too lenient towards nulls masks programming errors.
            throw new ArgumentNullException(nameof(obj));
        }

        /// <summary>
        /// If the object reference is not null, convert the object to a string, and add it as a query parameter.
        /// </summary>
        public ApiQueryParamBuilder MaybeWithObject(string paramName, object obj) // TODO: test
        {
            return (obj != null) ? With(paramName, obj.ToString()) : this;
        }

        /// <summary>
        /// When the condition is true, add the parameter.
        /// </summary>
        public ApiQueryParamBuilder WhenTrueInclude(bool condition, string paramName, string value)
        {
            return (condition) ? With(paramName, value) : this;
        }

        /// <summary>
        /// When the condition is true, add the parameter by calling the function to get the value string.
        /// </summary>
        public ApiQueryParamBuilder WhenTrueInclude(bool condition, string paramName, Func<string> parameterGetter)
        {
            return (condition) ? With(paramName, parameterGetter()) : this;
        }

        /// <summary>
        /// Return the resulting URL path and query parameters as a complete string.
        /// </summary>
        public override string ToString()
        {
            return this.url.Url;
        }

        /// <summary>
        /// Add a parameter and value pair using appropriate prefix, and inserting '='.
        /// </summary>
        private ApiQueryParamBuilder Append(string paramName, string value)
        {
            var prefix = (this.url.HasNoParameters) ? "?" : "&";
            this.url.Append(prefix + EscapedName(paramName) + "=" + EscapedValue(value));
            return this;
        }
    }
}
