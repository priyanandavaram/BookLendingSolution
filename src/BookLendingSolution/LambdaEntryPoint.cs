using Amazon.Lambda.AspNetCoreServer;
using BookLendingSolution.Interfaces;
using BookLendingSolution.Repository;
using BookLendingSolution.Service;

namespace BookLendingSolution
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>(); 
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddScoped<IBookService, BookService>();

            services.AddSingleton<IBookRepository, BookRepository>();
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