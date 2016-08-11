namespace HelloMicroservices
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Nancy;
    using Nancy.Owin;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public class Startup
    {
         public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.LiterateConsole().WriteTo.RollingFile("log-{Date}.txt").CreateLogger();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseMiddleware<TimerMiddleware>();
            app.UseOwin().UseNancy((opt => opt.Bootstrapper = new Bootstrapper(Log.Logger)));
            
        }
    }

    public class Bootstrapper : DefaultNancyBootstrapper
  {
    private Serilog.ILogger logger;
    public Bootstrapper(Serilog.ILogger logger)
    {
      this.logger = logger;
    }
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      container.Register(logger);
    }
  }
}
