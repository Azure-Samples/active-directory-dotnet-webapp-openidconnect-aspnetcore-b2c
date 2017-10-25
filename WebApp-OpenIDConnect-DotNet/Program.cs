using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApp_OpenIDConnect_DotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = WebHost.CreateDefaultBuilder(args)
               .CaptureStartupErrors(true)
               .UseSetting("detailedErrors", "true")
               .UseStartup<Startup>()
               .Build();

            webHost.Run();
        }
    }
}
