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
        /// Always include this parameter and string value.  If the string is null an exception is raised.
        /// </summary>
        public ApiQueryParamBuilder With(string paramName, string stringValue)
        {
            if (stringValue != null)
            {
                return new ApiQueryParamBuilder2(true, _query, this).Add(paramName, stringValue);
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
        public ApiQueryParamBuilder WithObject(string paramName, object obj)
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
        public ApiQueryParamBuilder MaybeWithObject(string paramName, object obj)
        {
            return (obj != null) ? With(paramName, obj.ToString()) : this;
        }

        /// <summary>
        /// When the condition is true, add the parameter by calling the function to get the value string.
        /// </summary>
        public ApiQueryParamBuilder WhenTrueInclude(bool condition, string paramName, Func<string> parameterGetter)
        {
            if (condition)
            {
                return With(paramName, parameterGetter());
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
