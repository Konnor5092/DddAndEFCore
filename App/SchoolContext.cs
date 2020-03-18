using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App
{
    public class SchoolContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext(string connectionString, bool useConsoleLogger)
        { 
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useConsoleLogger) 
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentID");
                x.Property(p => p.Email);
                x.Property(p => p.Name);
                x.HasOne(p => p.FavoriteCourse).WithMany();
            });

            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseID");
                x.Property(p => p.Name);
            });

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Calculus"},
                new Course { Id = 2, Name = "Chemistry"},
                new Course { Id = 3, Name = "Literature"},
                new Course { Id = 4, Name = "Trigonometry"},
                new Course { Id = 5, Name = "Microeconomics"});
           
            modelBuilder.Entity<Student>().HasData(
                new {Id = 1L, Name = "Alice", Email = "alice@gmail.com", FavoriteCourseId = 2L},
                new {Id = 2L, Name = "Bob", Email = "bob@outlook.com", FavoriteCourseId = 2L});     
        }
    }
}