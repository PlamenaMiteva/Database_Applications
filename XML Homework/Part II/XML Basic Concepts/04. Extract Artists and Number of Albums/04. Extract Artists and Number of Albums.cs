using System;
using System.Collections.Generic;
using System.Xml;

namespace _04.Extract_Artists_and_Number_of_Albums
{
    class ExtractArtistsAndNumberOfAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDataDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            var albumNodes = doc.DocumentElement.ChildNodes;
            Dictionary<string, int> artists = new Dictionary<string, int>() ;
            foreach (XmlNode albumNode in albumNodes)
            {
                string name = albumNode["catalogue:artist"].InnerText;
                if (!artists.ContainsKey(name))
                {
                    artists.Add(name, 1);
                }
                else
                {
                    artists[name]++;
                }
            }
            foreach (var artist in artists)
            {
                Console.WriteLine(artist.Key + " - " + artist.Value);
            }
        }
    }
}
