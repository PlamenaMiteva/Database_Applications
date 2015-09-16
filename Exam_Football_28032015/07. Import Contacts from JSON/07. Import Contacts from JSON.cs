using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using _06.Phonebook;

namespace _07.Import_Contacts_from_JSON
{
    class ImportContactsFromJson
    {
        static void Main()
        {
            var context = new PhonebookEntities();
            string text = System.IO.File.ReadAllText("../../contacts.json");
            JArray arr = JArray.Parse(text);
            foreach (JToken contact in arr)
            {
                string name = null;
                string company = null;
                string position = null;
                string site = null;
                string note = null;
                if (contact["name"] != null)
                {
                    name = contact["name"].ToString();
                }
                else if (contact["firstName"] != null)
                {
                    name = contact["firstName"].ToString() + " " + contact["lastName"].ToString();
                }
                else
                {
                    Console.WriteLine("Error: Name is required");
                    continue;
                }
                if (contact["company"] != null)
                {
                    company = contact["company"].ToString();
                }
                if (contact["position"] != null)
                {
                    position = contact["position"].ToString();
                }
                if (contact["site"] != null)
                {
                    site = contact["site"].ToString();
                }
                if (contact["notes"] != null)
                {
                    note = contact["notes"].ToString();
                }
                Contact person = new Contact()
                {
                    Name = name,
                    Company = company,
                    Position = position,
                    Site = site,
                    Notes = note
                };
                if (contact["phones"] != null)
                {
                    var query = contact["phones"].Select(c => c.ToString());
                    foreach (var phone in query)
                    {
                        Phone num = new Phone()
                        {
                            PhoneNumber = phone.ToString()
                        };
                        person.Phones.Add(num);
                    }
                }
                if (contact["emails"] != null)
                {
                    var mails = contact["emails"].Select(e => e.ToString());
                    foreach (var mail in mails)
                    {
                        Email email = new Email()
                        {
                            EmailAddress = mail
                        };
                        person.Emails.Add(email);
                    }
                }
                context.Contacts.Add(person);
                context.SaveChanges();
                Console.WriteLine("Contact {0} imported", name);
            }
        }
    }
}
