using StudentsSystemModels;

namespace StudentSystemData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemData.StudentSystemEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentSystemData.StudentSystemEntities context)
        {
            context.Students.AddOrUpdate(
                s => s.Name,
              new Student { 
                  Name = "Andrew Peters", 
                  PhoneNumber = "0888673465", 
                  Birthday = new DateTime(2000, 03, 30), 
                  RegistrationDate = DateTime.Now
              });
            context.SaveChanges();
            context.Students.AddOrUpdate
                (s => s.Name, 
                new Student
            {
                Name = "Brice Lambson",
                PhoneNumber = "088853543275",
                Birthday = new DateTime(1998, 04, 06),
                RegistrationDate = DateTime.Now
            });
            context.SaveChanges();
            context.Students.AddOrUpdate(
                s => s.Name, 
                new Student
               {
                   Name = "Rowan Miller",
                   PhoneNumber = "0888563543257",
                   Birthday = new DateTime(2005, 01, 16),
                   RegistrationDate = DateTime.Now
               });
            context.SaveChanges();

            context.Courses.AddOrUpdate(
                c => c.Name,
                new Course()
                {
                    Name = "JavaScript Basics",
                    Price = 200,
                    StartDate = new DateTime(2015, 01, 16),
                    EndDate = new DateTime(2015, 02, 16)
                });
            context.SaveChanges();

            context.Courses.AddOrUpdate(
                c => c.Name,
                new Course()
                {
                    Name = "Java Basics",
                    Price = 230,
                    StartDate = new DateTime(2015, 04, 26),
                    EndDate = new DateTime(2015, 05, 27)
                });
            context.SaveChanges();

            context.Courses.AddOrUpdate(
                c => c.Name,
                new Course()
                {
                    Name = "C# Basics",
                    Price = 150,
                    StartDate = new DateTime(2015, 03, 10),
                    EndDate = new DateTime(2015, 05, 02)
                });
            context.SaveChanges();

            context.Resources.AddOrUpdate(
                r => r.Name,
                new Resource()
                {
                    Name = "C# Basics Lecture",
                    URL = "www.softuni.bg",
                    CourseId = 3
                });
            context.SaveChanges();

            context.Homeworks.AddOrUpdate(
                h => h.Content,
                new Homework()
                {
                    Content = "..................",
                    ContentType = "application.pdf",
                    SubmissionDate = new DateTime(2015, 05, 02),
                    CourseId = 1,
                    StudentId =  1
                });
            context.SaveChanges();
        }
    }
}
