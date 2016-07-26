using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HelloMicroservices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigBuild(args)
            .Build();

            var host = new WebHostBuilder()
              .UseConfiguration(config)
              .UseIISIntegration()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }

        private static IConfigurationBuilder ConfigBuild(string[] args)
        {
            return new ConfigurationBuilder()
                    .AddCommandLine(args);
        }
    }
}
