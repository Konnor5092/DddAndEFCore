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
            string result = Execute(x => x.CheckStudentFavoriteCourse(1, 2));
        }

        private static string Execute(Func<StudentController, string> func)
        {
            string connectionString = SchoolContextFactory.GetConnectionString();

            using (var context = new SchoolContext(connectionString, true))
            {
                var controller = new StudentController(context);
                return func(controller);
            }
        }
    }
}
