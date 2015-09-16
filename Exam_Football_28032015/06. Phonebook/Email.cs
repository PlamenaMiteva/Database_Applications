using System;
using System.ComponentModel.DataAnnotations;

namespace _06.Phonebook
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}
