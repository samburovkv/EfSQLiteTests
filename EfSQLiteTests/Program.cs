using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfSQLiteTests
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();
            var dbContext = hostBuilder.Services.GetRequiredService<ApplicationDbContext>();

            var person = dbContext.Persons.FirstOrDefault(x => x.Id == "6c7a5476-0266-459e-8229-544483379108");

            person.Numbers.Add(98);
            await dbContext.SaveChangesAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(AppDomain.CurrentDomain.BaseDirectory)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(
                        (provider, options) =>
                        {
                            options.UseSqlite("Data Source=app.db");
                        });
                });
    }
}