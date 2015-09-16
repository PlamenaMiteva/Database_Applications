using System;
using System.Collections.Generic;
using System.Xml;

namespace _06.DOM__Delete_Albums
{
    class DeleteAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("catalogue", "https://gist.github.com/jasonbaldridge/2597611");
            XmlNodeList albumList = doc.SelectNodes("/catalogue:albums/catalogue:album", namespaceManager);
            foreach (XmlNode albumNode in albumList)
            {
                double price = Double.Parse(albumNode.SelectSingleNode("catalogue:price", namespaceManager).InnerText);
                if (price > 20)
                {
                    albumNode.ParentNode.RemoveChild(albumNode);
                }
                doc.Save("catalogue-new.xml");
            }
        }
    }
}
