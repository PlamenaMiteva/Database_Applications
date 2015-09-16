using System;
using System.ComponentModel.DataAnnotations;

namespace _06.Phonebook
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
