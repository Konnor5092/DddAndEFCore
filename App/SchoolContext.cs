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

            modelBuilder.Entity<Enrollment>(x => 
            {
                x.ToTable("Enrollment").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("EnrollmentId");
                x.HasOne(p => p.Course).WithMany();
                x.HasOne(p => p.Student).WithMany();
                x.Property(p => p.Grade);
            });

            modelBuilder.Entity<Course>().HasData(
                new { Id = 1L, Name = "Calculus"},
                new { Id = 2L, Name = "Chemistry"},
                new { Id = 3L, Name = "Literature"},
                new { Id = 4L, Name = "Trigonometry"},
                new { Id = 5L, Name = "Microeconomics"});
           
            modelBuilder.Entity<Student>().HasData(
                new {Id = 1L, Name = "Alice", Email = "alice@gmail.com", FavoriteCourseId = 2L},
                new {Id = 2L, Name = "Bob", Email = "bob@outlook.com", FavoriteCourseId = 2L});     
        }
    }
}