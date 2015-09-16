using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _05.Mountains_Code_First
{
    public class Country
    {

        public Country()
        {
            this.Mountains = new HashSet<Mountain>();
        }
        [Key]
        [StringLength(2, MinimumLength = 2)]
        [Column(TypeName = "char")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }


        public virtual ICollection<Mountain> Mountains { get; set; }
    }
}

