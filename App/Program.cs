using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            ILoggerFactory loggerFactory = CreateLoggerFactory();

            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();

            using (var context = new SchoolContext(optionsBuilder.Options))
            {
                Student student = context.Students.Find(1L);
            }
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });
        }
    }
}
