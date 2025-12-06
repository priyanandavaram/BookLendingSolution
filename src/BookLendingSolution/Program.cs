namespace BookLendingSolution
{

    public partial class Program
    {
        //public static void ConfigureApp(IApplicationBuilder app)
        //{

        //}

        //public static WebApplication BuildWebApp(string[] args)
        //{
        //    var builder = WebApplication.CreateBuilder(args);
        //    var startup = new Startup();

        //    startup.ConfigureServices(builder.Services);

        //    var app = builder.Build();

        //    startup.Configure(app, app.Environment);

        //    app.Run();

        //    // builder.Services.AddControllers();
        //    // builder.Services.AddEndpointsApiExplorer();
        //    // builder.Services.AddSwaggerGen();
        //    // builder.Services.AddScoped<Interfaces.IBookService, Service.BookService>();
        //    // builder.Services.AddSingleton<Interfaces.IBookRepository, Repository.BookRepository>();


        //    // var app = builder.Build();


        //    //app.UseSwagger();
        //    //app.UseSwaggerUI();
        //    //app.UseRouting();
        //    //app.MapControllers();

        //    return app;
        //}

        //public static void Main(string[] args)
        //{
        //    var app = BuildWebApp(args);
        //    app.Run();
        //}
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
}
