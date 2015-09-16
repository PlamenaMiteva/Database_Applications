using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace _05.XPath_Extract_Artists_and_Number_of_Albums
{
    internal class XPathExtractArtistsAndNumberOfAlbums
    {
        private static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("catalogue", "https://gist.github.com/jasonbaldridge/2597611");
            Dictionary<string, int> artists = new Dictionary<string, int>();
            XmlNodeList albumList = doc.SelectNodes("/catalogue:albums/catalogue:album", namespaceManager);
            foreach (XmlNode albumNode in albumList)
            {
                string  artist = (albumNode.SelectSingleNode("catalogue:artist", namespaceManager).InnerText);
                if (!artists.ContainsKey(artist))
                {
                    artists.Add(artist, 1);
                }
                else
                {
                    artists[artist]++;
                }
            }
            foreach (var artist in artists)
            {
                Console.WriteLine(artist.Key + " - " + artist.Value);
            }
        }
    }
}

