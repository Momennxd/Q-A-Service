using Core_Layer.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    /// <summary>
    /// Created to use Dependency Inversion Principle(DIP)
    /// </summary>
    public static class clsService
    {
        public static IDbContextFactory<AppDbContext>? contextFactory { get; }

        public static AppDbContext Context { get; }

        static clsService()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            
            string? connectionString = configuration.GetConnectionString("MyConnection");

            ServiceCollection service = new ServiceCollection();

            service.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));
            

            IServiceProvider serviceProvider = service.BuildServiceProvider();

            contextFactory = serviceProvider.GetService<IDbContextFactory<AppDbContext>>();

            Context = clsService.contextFactory!.CreateDbContext();
        }
    }
}
