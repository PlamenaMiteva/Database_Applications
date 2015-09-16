using System;

namespace StudentsSystemModels
{
    public class Homework
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int CourseId { get; set; }
        public virtual Course HomeworkCourse { get; set; }
        public int StudentId { get; set; }
        public virtual Course StudentHomework { get; set; }
    }
}
