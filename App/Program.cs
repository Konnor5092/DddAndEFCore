using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            Console.WriteLine("I am a spoon");

            using (var context = new SchoolContext(connectionString, true))
            {
                Student student = context.Students
                    //.Include(f => f.FavoriteCourse)
                    .SingleOrDefault(x => x.Id == 1);

                context.SaveChanges();
            }
        }
    }
}
