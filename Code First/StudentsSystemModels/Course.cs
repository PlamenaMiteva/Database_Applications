using System;
using System.Collections.Generic;


namespace StudentsSystemModels
{
    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Resource> resources;
        private ICollection<Homework> homeworks;
        public Course()
        {
            this.students = new HashSet<Student>();
            this.Resources = new HashSet<Resource>();
            this.Homeworks = new HashSet<Homework>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }
        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
