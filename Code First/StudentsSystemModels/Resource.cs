using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace StudentsSystemModels
{
    public class Resource
    {

        private ICollection<License> licenses;
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }
        public string URL { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }
        
    }
}
