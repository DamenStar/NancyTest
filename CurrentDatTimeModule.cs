namespace HelloMicroservices
{
  using System;
  using Nancy;
  using Serilog;

  public class CurrentDateTimeModule
    : NancyModule
  {
    private readonly ILogger _logger;
    public CurrentDateTimeModule(ILogger logger)
    {
      _logger = logger;
      
      Get("/", _ =>  {
          _logger.Debug("In nancy");
          return DateTime.UtcNow;
      });
       Get("/test/{name}", args => new Person() { Name = args.name });
       
    }
     public class Person
    {
        public string Name { get; set; }
    }

  }
}
