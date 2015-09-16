using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSystemData;

namespace StudentSystem.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new StudentSystemEntities();



            //var students = context.Students.
            //               Join (context.Homeworks,
            //               (s => s.Id),
            //               (h => h.StudentId), 
            //               (s,h) => new {
            //                   StudentName = s.Name, 
            //                   Content = h.Content,
            //                   ContentType = h.ContentType
            //               });
            //foreach (var student in students)
            //{
            //    Console.WriteLine(student.StudentName + " - " + student.Content + " - " + student.ContentType);
            //}





            //var courses = context.Courses.Join(context.Resources,
            //    (c => c.Id),
            //               (r => r.CourseId), 
            //               (c,r) => new
            //               {
            //                   CourseName = c.Name,
            //                   CourseDescription = c.Description,
            //                   c.StartDate,
            //                   c.EndDate,
            //                   Resource = r.Name,
            //                   Resourcetype = r.ResourceType,
            //                   URL = r.URL
            //               }
            //    ).OrderBy(c=>c.StartDate).ThenByDescending(c=>c.EndDate);

            //foreach (var course in courses)
            //{
            //    Console.WriteLine(course.CourseName + " - " + course.CourseDescription + " - " + course.Resource + " - " + course.Resourcetype + " - " + course.URL);
            //}




            //var courses = context.Courses.Where(c => c.Resources.Count >= 5)
            //    .OrderByDescending(c => c.Resources.Count())
            //    .ThenByDescending(c => c.StartDate).Select(c => new 
            //    {
            //        c.Name,
            //        ResourcesCount = c.Resources.Count()
            //    });

            //foreach (var course in courses)
            //{
            //    Console.WriteLine(course.Name + ": " + course.ResourcesCount);
            //}



            //DateTime date = new DateTime(2015, 05, 01);
            //var courses = from x in (from s in context.Courses.Where(c => c.StartDate <= date && c.EndDate >= date)
            //    select new
            //    {
            //        CourseName = s.Name,
            //        s.StartDate,
            //        s.EndDate,
            //        Duration = SqlFunctions.DateDiff("day", s.StartDate, s.EndDate),
            //        NumberOfStudents = s.Students.Count
            //    }).OrderByDescending( x => x.NumberOfStudents).ThenByDescending(x => x.Duration) 
            // select new
            //    {
            //        CourseName = x.CourseName,
            //        x.StartDate,
            //        x.EndDate,
            //        Duration = x.Duration,
            //        NumberOfStudents = x.NumberOfStudents
            //    };
            //foreach (var course in courses)
            //{
            //   Console.WriteLine(course.CourseName + "  " +course.StartDate + "  " +course.EndDate
            //       + "  " + course.Duration + " days " + course.NumberOfStudents + " students"); 
            //}




            var students = context.Students.Select(s => new
            {
                s.Name,
                NumOFCourses = s.Courses.Count(),
                TotalPricePaid = s.Courses.Sum(c => c.Price),
                AvgPrice = s.Courses.Average(c => c.Price)
            }).OrderByDescending(s => s.TotalPricePaid).ThenByDescending(s => s.AvgPrice);

            foreach (var student in students)
            {
                Console.WriteLine("Student Name: {0}, Number Of Courses: {1}, Total Price {2} lv, Avg Price: {3} BGN",
                    student.Name, student.NumOFCourses, student.TotalPricePaid, student.AvgPrice);
            }

        }
    }
}

