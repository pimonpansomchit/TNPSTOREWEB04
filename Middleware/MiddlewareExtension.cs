namespace TNPSTOREWEB.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseQrcodeFileMiddelware
                   (this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<QrcodeFileMiddelware>();
        }
        public static IApplicationBuilder UseIPQRcodeModuleMiddleware
          (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPQRcodeModuleMiddleware>();
        }
    }
}
