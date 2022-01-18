
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

        public override string ToString()
        {
            return _query.QueryString;
        }
    }
}
