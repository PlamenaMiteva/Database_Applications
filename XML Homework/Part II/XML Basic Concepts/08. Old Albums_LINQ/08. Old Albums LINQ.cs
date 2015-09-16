using System;
using System.Linq;
using System.Xml.Linq;

namespace _08.Old_Albums_LINQ
{
    class OldAlbums
    {
        static void Main()
        {
            XDocument xmlDoc = XDocument.Load("../../../Catalogue.xml");
            XNamespace ns = "https://gist.github.com/jasonbaldridge/2597611";
            int currentYear = (DateTime.Now).Year - 5;
            var albums =
                from album in xmlDoc.Descendants(ns+ "album")
                where (int.Parse(album.Element(ns+ "year").Value) < currentYear) 
                select new
                {
                    Title = album.Element(ns+ "name").Value,
                    Price = album.Element(ns+ "price").Value
                };
            foreach (var album in albums)
            {
                Console.WriteLine(album);
            }

        }
    }
}
