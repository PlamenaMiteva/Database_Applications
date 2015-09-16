using System;
using System.Collections.Generic;
using System.Xml;

namespace _07.Old_Albums
{
    internal class OldAlbums
    {
        private static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("catalogue", "https://gist.github.com/jasonbaldridge/2597611");
            
            XmlNodeList albumList = doc.SelectNodes("/catalogue:albums/catalogue:album", namespaceManager);
            Console.WriteLine("Albums published five or more years ago:");
            foreach (XmlNode albumNode in albumList)
            {
                int year = int.Parse(albumNode.SelectSingleNode("catalogue:year", namespaceManager).InnerText);
                int currentYear = (DateTime.Now).Year-5;
                if (year<=currentYear)
                {
                    Console.WriteLine(albumNode.SelectSingleNode("catalogue:name", namespaceManager).InnerText + ", "+
                        albumNode.SelectSingleNode("catalogue:price", namespaceManager).InnerText + "EUR");
                }
            }
          }
    }
}

