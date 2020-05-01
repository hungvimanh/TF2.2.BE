using Microsoft.Extensions.Options;
using System;
using TwelveFinal.Repositories.Models;
using Z.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DataSeeding
{
    class Program
    {
        private static TFContext tFContext;
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
            string connectionString = config.GetConnectionString("TFContext");
            var options = new DbContextOptionsBuilder<TFContext>()
                .UseSqlServer(connectionString)
                .Options;
            tFContext = new TFContext(options);
            EntityFrameworkManager.ContextFactory = DbContext => tFContext;

            GlobalInit globalInit = new GlobalInit(tFContext);
            globalInit.Init();
            Console.WriteLine("Data Seeding Finished");
        }
    }
}
