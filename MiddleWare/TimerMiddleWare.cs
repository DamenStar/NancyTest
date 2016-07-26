using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloMicroservices
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;

        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            context.Response.Headers.Add("ExecutionTime", new[] { stopWatch.Elapsed.Milliseconds.ToString() });
            await _next(context);

        }
    }
}