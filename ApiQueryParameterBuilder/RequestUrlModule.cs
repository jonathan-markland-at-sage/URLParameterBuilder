namespace ApiQueryParameterBuilder
{
    public static class RequestUrlModule
    {
        public static ApiQueryParamBuilder RequestUrl(string pathPart)
        {
            return new ApiQueryParamBuilder(pathPart);
        }
    }
}
