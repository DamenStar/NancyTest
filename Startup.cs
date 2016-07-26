namespace HelloMicroservices
{
    using Microsoft.AspNetCore.Builder;
    using Nancy.Owin;
    
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<TimerMiddleware>();
            app.UseOwin().UseNancy();
            

        }
    }
}
