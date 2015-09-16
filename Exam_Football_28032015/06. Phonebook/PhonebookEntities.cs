namespace _06.Phonebook
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookEntities : DbContext
    {
        public PhonebookEntities()
            : base("name=PhonebookEntities")
        {
        }

        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Email> Emails{ get; set; }
        public virtual DbSet<Contact> Contacts{ get; set; }
    }
}