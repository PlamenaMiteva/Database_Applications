using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace _06.Phonebook
{
    class Phonebook
    {
        static void Main()
        {
            var context = new PhonebookEntities();
            Console.WriteLine(context.Contacts.Count());
            Contact petar = new Contact()
            {
                Name = "Peter Ivanov",
                Position = "CTO",
                Company = "Smart Ideas",
                Emails = new List<Email>{
        new Email(){EmailAddress = "peter@gmail.com"},
        new Email(){EmailAddress ="peter_ivanov @yahoo.com"}
    },
                Phones = new List<Phone>{
        new Phone()
        {PhoneNumber = "(+359) 2 22 22 22"},
        new Phone()
        {PhoneNumber = "+359 88 77 88 99"}
    },
                Site = "http: //blog.peter.com",
                Notes = "Friend from school"
            };

            Contact maria = new Contact()
            {
                Name = "Maria",
                Phones = new List<Phone>
                {
                    new Phone()
                    {PhoneNumber = "+359 22 33 44 55"}
                }
            };

            Contact angie = new Contact()
            {
                Name = "Angie Stanton",
                Site = "http://angiestanton.com",
                Emails = new List<Email>
                {
                    new Email()
                    {EmailAddress = "info@angiestanton.com"}
                }
            };
            context.Contacts.Add(petar);
            context.Contacts.Add(maria);
            context.Contacts.Add(angie);
            context.SaveChanges();
        }
    }
}
