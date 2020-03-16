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
            string connectionString = SchoolContextFactory.GetConnectionString();

            using (var context = new SchoolContext(connectionString, true))
            {
                Student student = context.Students.Find(1L);
                student.Name += 2;
                student.Email += 2;

                context.SaveChanges();
            }
        }
    }
}
