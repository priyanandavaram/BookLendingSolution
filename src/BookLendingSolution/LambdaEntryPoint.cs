using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace BookLendingSolution
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Startup>(); // for .NET 6/7
        }
    }
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Nothing to do here usually unless you're using DI overrides
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // The Program.cs already configures the pipeline,
            // this simply forwards to your main app
            Program.ConfigureApp(app);
        }
    }
}
