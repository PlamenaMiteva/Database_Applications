using System;
using System.Data.Entity;
using System.Linq;

namespace _06.Code_First_Phonebook
{
    class Phonebook
    {
        static void Main()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhonebookEntities>());
            var context = new PhonebookEntities();
            //context.Users.Add(new User()
            //{
            //    Username = "VGeorgiev",
            //    FullName = "Vladimir Georgiev",
            //    PhoneNumber = "0894545454"
            //});
            //context.Users.Add(new User()
            //{
            //    Username = "Nakov",
            //    FullName = "Svetlin Nakov",
            //    PhoneNumber = "0897878787"
            //});
            //context.Users.Add(new User()
            //{
            //    Username = "Ache",
            //    FullName = "Angel Georgiev",
            //    PhoneNumber = "0897121212"
            //});
            //context.Users.Add(new User()
            //{
            //    Username = "Alex",
            //    FullName = "Alexandra Svilarova",
            //    PhoneNumber = "0894151417"
            //});
            //context.Users.Add(new User()
            //{
            //    Username = "Petya",
            //    FullName = "Petya Grozdarska",
            //    PhoneNumber = "0895464646"
            //});

            //context.Channels.Add(new Channel()
            //{
            //    Name = "Malinki"
            //});
            //context.Channels.Add(new Channel()
            //{
            //    Name = "SoftUni"
            //});
            //context.Channels.Add(new Channel()
            //{
            //    Name = "Admins"
            //});
            //context.Channels.Add(new Channel()
            //{
            //    Name = "Programmers"
            //});
            //context.Channels.Add(new Channel()
            //{
            //    Name = "Geeks"
            //});
            //context.SaveChanges();

            //context.ChannelMessages.Add(new ChannelMessage()
            //{
            //    Channel = context.Channels.FirstOrDefault(c=>c.Name=="Malinki"),
            //    Content = "Hey dudes, are you ready for tonight?",
            //    DateTime = DateTime.Now,
            //    User =context.Users.FirstOrDefault(u=>u.Username=="Petya")
            //});
            //context.ChannelMessages.Add(new ChannelMessage()
            //{
            //    Channel = context.Channels.FirstOrDefault(c => c.Name == "Malinki"),
            //    Content = "Hey Petya, this is the SoftUni chat.",
            //    DateTime = DateTime.Now,
            //    User = context.Users.FirstOrDefault(u => u.Username == "VGeorgiev")
            //});
            //context.ChannelMessages.Add(new ChannelMessage()
            //{
            //    Channel = context.Channels.FirstOrDefault(c => c.Name == "Malinki"),
            //    Content = "Hahaha, we are ready!",
            //    DateTime = DateTime.Now,
            //    User = context.Users.FirstOrDefault(u => u.Username == "Nakov")
            //});
            //context.ChannelMessages.Add(new ChannelMessage()
            //{
            //    Channel = context.Channels.FirstOrDefault(c => c.Name == "Malinki"),
            //    Content = "Oh my god. I mean for drinking beers!",
            //    DateTime = DateTime.Now,
            //    User = context.Users.FirstOrDefault(u => u.Username == "Petya")
            //});
            //context.ChannelMessages.Add(new ChannelMessage()
            //{
            //    Channel = context.Channels.FirstOrDefault(c => c.Name == "Malinki"),
            //    Content = "We are sure!",
            //    DateTime = DateTime.Now,
            //    User = context.Users.FirstOrDefault(u => u.Username == "VGeorgiev")
            //});
            //context.SaveChanges();

            var query = context.Channels.Select(c => new
            {
                c.Name,
                Messages= c.ChannelMessages
            }).ToList();
            foreach (var channel in query)
            {
                Console.WriteLine(channel.Name);
                Console.WriteLine("--Messages--");
                foreach (var message in channel.Messages)
                {
                    Console.WriteLine("Content: {0}, DateTime: {1}, User: {2}",message.Content, message.DateTime, message.User.Username);
                }
            }



        }
    }
}
