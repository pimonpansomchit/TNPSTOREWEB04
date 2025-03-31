namespace TNPSTOREWEB.Middleware
{
    public class QrcodeFileMiddelware
    {
        private RequestDelegate _next;

        public QrcodeFileMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<p>Process files with .aspx extension</p>");

            // Any Redirection logic can be return here.
        }
       
    }
}
