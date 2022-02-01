using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiQueryParameterBuilder
{
    public class ApiQueryParamWhenCondition
    {
        private readonly bool condition;
        private readonly ApiQueryParamBuilder apiQueryParamBuilder;

        internal ApiQueryParamWhenCondition(bool condition, ApiQueryParamBuilder apiQueryParamBuilder)
        {
            this.condition = condition;
            this.apiQueryParamBuilder = apiQueryParamBuilder;
        }

        /// <summary>
        /// Specify parameter and value to be appended, when the condition is true.
        /// </summary>
        public ApiQueryParamBuilder Include(string paramName, string value)
        {
            return (condition) 
                ? apiQueryParamBuilder.With(paramName, value) 
                : apiQueryParamBuilder;
        }

        /// <summary>
        /// Specify parameter and obtain value to be appended, when the condition is true.
        /// The function will be called to obtain the value only when the condition is true.
        /// </summary>
        public ApiQueryParamBuilder Include(string paramName, Func<string> parameterGetter)
        {
            return (condition) 
                ? apiQueryParamBuilder.With(paramName, parameterGetter()) 
                : apiQueryParamBuilder;
        }
    }
}
