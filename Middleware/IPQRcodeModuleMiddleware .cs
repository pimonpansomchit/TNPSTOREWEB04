namespace TNPSTOREWEB.Middleware
{
    public class IPQRcodeModuleMiddleware
    {
        private RequestDelegate _next;
        public IPQRcodeModuleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<p>Begin request</p>");
            await _next.Invoke(context);
            await context.Response.WriteAsync("<p>End request</p>");
        }
    }
}
