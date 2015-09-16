using System;
using System.Collections.Generic;
using System.Xml;

namespace _03.Extract_All_Artists_Alphabetically
{
    class ExtractAllArtistsAlphabetically
    {
        static void Main()
        {
            XmlDocument doc = new XmlDataDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            var albumNodes = doc.DocumentElement.ChildNodes;
            SortedSet<string> artists= new SortedSet<string>();
            foreach (XmlNode albumNode in albumNodes)
            {
                artists.Add(albumNode["catalogue:artist"].InnerText);
            }
            foreach (var artist in artists)
            {
                Console.WriteLine(artist);
            }
        }
    }
}
