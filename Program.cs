
using example_app.Database.Connector;
using example_csharp_app;
using example_csharp_app.Aggregators;
using example_csharp_app.Interfaces;
using example_csharp_app.Repositories;
using example_csharp_app.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Program 
{
    public static class Program
    {
        private static void SetupDi(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookAggregator, BookAggregator>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddDbContext<AppDbContext>(
                (provider, builder) => 
                {
                    //var settings = provider.GetRequiredService<IOptions<DbSettings>>(); // эта версия лучше
                    var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                    builder.UseNpgsql(connectionString);
                }, 
                ServiceLifetime.Transient
            );
        }

        public static int Main(string[] args)
        {
            try
            {
                var configurationBuilder = (new ConfigurationBuilder())
                                            .SetBasePath(Environment.CurrentDirectory)
                                            .AddJsonFile("appsettings.json", false, true)
                                            .AddJsonFile("appsettings.{environmentName}", true, true)
                                            .AddEnvironmentVariables()
                                            .AddCommandLine(args);

                var builder = WebApplication.CreateBuilder(args);
                var configuration = configurationBuilder.Build();
                
                builder.Services.AddControllers();
                builder.Services.AddSwaggerGen();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
                
                SetupDi(builder.Services, configuration);
                
                var app = builder.Build();
                app.UseCors("AllowAll");
                app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                //if(app.Environment.IsDevelopment())
                //{
                    app.UseSwagger();
                    app.UseSwaggerUI();
                //}
                app.MapControllers();
                app.UseMiddleware<ErrorLogMiddleware>();
                //app.UseHttpsRedirection();
                
                app.Run();

            } catch (Exception)
            {
                // log
            }
            finally
            {
                // flush log
            }

            return 0;
            
        }
    }
}



