using StudentsSystemModels;
using StudentSystemData.Migrations;

namespace StudentSystemData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentSystemEntities : DbContext
    {
        // Your context has been configured to use a 'StudentSystemEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'StudentSystemData.StudentSystemEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StudentSystemEntities' 
        // connection string in the application configuration file.
        public StudentSystemEntities()
            : base("name=StudentSystemEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemEntities, Configuration>());
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}