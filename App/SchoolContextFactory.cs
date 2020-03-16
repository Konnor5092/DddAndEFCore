using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace App
{
    public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            //optionsBuilder.UseSqlServer(GetConnectionString());

            return new SchoolContext(GetConnectionString(), true);
        }

        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration["ConnectionString"];
        }
    }
}