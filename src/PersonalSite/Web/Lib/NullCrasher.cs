namespace Web.Lib
{
    public static class NullCrasher
    {
        public static HttpContext HttpContextOrThrow(this IHttpContextAccessor hca)
        {
            var ctx = hca.HttpContext;
            if(ctx is null)
            {
                throw new InvalidOperationException("HttpContext is required, but was null");
            }
            else
            {
                return ctx;
            }
        }
    }
}
