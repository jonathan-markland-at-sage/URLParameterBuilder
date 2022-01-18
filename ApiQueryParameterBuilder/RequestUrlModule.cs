namespace ApiQueryParameterBuilder
{
    public static class RequestUrlModule
    {
        /// <summary>
        /// Build a URL from an initial path, then add parameters.
        /// </summary>
        /// <param name="pathPart">Optional initial path</param>
        /// <returns>Builder object allowing further query items to be added. Use ToString() to finish.</returns>
        public static ApiQueryParamBuilder RequestUrl(string pathPart)
        {
            return new ApiQueryParamBuilder(pathPart);
        }
    }
}
