namespace Memory.WebUI.MiddleWare
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string content = $"İstek atılan metot : {httpContext.Request.Method}\n Yol: {httpContext.Request.Path.Value}\n Statu Kod : {httpContext.Response.StatusCode}";

         

            try
            {
                File.AppendAllText("Logging.txt", content);
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                content += ex.Message;
                File.AppendAllText("Logging.txt", content);
               
            }
        }

    }
}
