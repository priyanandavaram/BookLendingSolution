namespace BookLendingSolution
{

    public partial class Program
    {
        public static void ConfigureApp(IApplicationBuilder app)
        {
         
        }

        public static WebApplication BuildWebApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Interfaces.IBookService, Service.BookService>();
            builder.Services.AddSingleton<Interfaces.IBookRepository, Repository.BookRepository>();

            var app = builder.Build();

           app.UseSwagger();
           app.UseSwaggerUI();
           app.UseRouting();
           app.MapControllers();

           ConfigureApp(app);

           return app;
        }

        public static void Main(string[] args)
        {
            var app = BuildWebApp(args);
            app.Run();
        }
    }
}
