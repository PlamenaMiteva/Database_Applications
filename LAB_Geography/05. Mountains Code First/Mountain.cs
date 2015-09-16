using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05.Mountains_Code_First
{
    public class Mountain
    {

        public Mountain()
        {
            this.Peaks = new HashSet<Peak>();
            this.Countries = new HashSet<Country>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Peak> Peaks { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
