using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using _06.Code_First_Phonebook;

namespace _07.Import_User_Messages
{
    class ImportUserMessages
    {
        static void Main()
        {
            var context = new PhonebookEntities();
            string text = System.IO.File.ReadAllText("../../messages.json");
            JArray arr = JArray.Parse(text);

            foreach (JToken message in arr)
            {
                string content = null;
                DateTime date = new DateTime();
                string recipient = null;
                string sender = null;
                try
                {
                    content = message["content"].ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Content is required");
                    continue;
                }
                try
                {
                    recipient = message["recipient"].ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Recipient is required");
                    continue;
                }
                try
                {
                    sender = message["sender"].ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Sender is required");
                    continue;
                }
                try
                {
                    date = Convert.ToDateTime(message["datetime"].ToString());
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Date is required");
                    continue;
                }
                if (content != null && sender != null && recipient != null && date != DateTime.MinValue)
                {
                    context.UserMesseges.Add(new UserMessage()
                    {
                        Content = content,
                        DateTime = date,
                        Recipient = context.Users.FirstOrDefault(r => r.Username == recipient),
                        Sender = context.Users.FirstOrDefault(r => r.Username == sender)
                    });
                    context.SaveChanges();
                    Console.WriteLine("Message \"{0}\" imported", content);
                }

            }
        }
    }
}
