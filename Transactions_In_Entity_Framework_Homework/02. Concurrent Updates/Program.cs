using System;
using System.Data.Entity.Core;
using System.Linq;
using NewsSystem.Data;

namespace _02.Concurrent_Updates
{
    class Concurrent_Updates
    {
        static void Main()
        {
            Console.WriteLine("Application started");
            var context = new NewsEntities();
            var firstNews = context.News.First();
            Console.WriteLine("Text from DB: {0}", firstNews.Content);
            Console.Write("Enter the corrected text:");
            string input = Console.ReadLine();
            firstNews.Content = input;

            var context2 = new NewsEntities();
            var firstNewssecUser = context2.News.First();
            Console.WriteLine("Text from DB: {0}", firstNewssecUser.Content);
            Console.Write("Enter the corrected text:");
            string input2 = Console.ReadLine();

            context.SaveChanges();
            Console.WriteLine("Changes successfully saved in the DB.");
           
            try
            {
               firstNewssecUser.Content = input2;
               context2.SaveChanges();
               Console.WriteLine("Changes successfully saved in the DB.");
            }
            catch (Exception)
            {
                var context3 = new NewsEntities();
                var firstNewsthirdUser = context3.News.First();
                Console.WriteLine("Conflict! Text from DB: {0}.", firstNewsthirdUser.Content);
                Console.Write("Enter the corrected text:");
                string newInput = Console.ReadLine();
                firstNewsthirdUser.Content = newInput;
                context3.SaveChanges();
                Console.WriteLine("Changes successfully saved in the DB.");
            }
        }
    }
}
