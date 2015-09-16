using System;
using NewsModels;
using NewsSystem.Data;

namespace NewsSystemConsoleClient
{
    class NewsDB
    {
        static void Main()
        {
            var context = new NewsEntities();
            var firstNews = new News()
            {
                Content = "Turkey's air force is attacking Islamic State (IS) positions in Syria and Kurdish PKK militants in northern Iraq to defend the country's security, Turkish PM Ahmet Davutoglu says."
            };
            context.News.Add(firstNews);
            context.SaveChanges();
            var secNews = new News()
            {
                Content = "The Pentagon has urged US citizens not to carry out armed patrols outside military recruitment centres."
            };
            context.News.Add(secNews);
            context.SaveChanges();
            var thirdNews = new News()
            {
                Content = "A man has been killed by a shark off the Tasmanian coast, the first such death in the region for 17 years."
            };
            context.News.Add(thirdNews);
            context.SaveChanges();
 
        }
    }
}
