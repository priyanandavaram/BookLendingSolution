using Amazon.Lambda.AspNetCoreServer;

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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<Interfaces.IBookService, Service.BookService>();
            services.AddSingleton<Interfaces.IBookRepository, Repository.BookRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
