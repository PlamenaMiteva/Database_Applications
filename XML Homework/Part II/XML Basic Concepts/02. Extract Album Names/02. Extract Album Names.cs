using System;
using System.Xml;

namespace _02.Extract_Album_Names
{
    class ExtractAlbumNames
    {
        static void Main()
        {
            XmlDocument doc = new XmlDataDocument();
            doc.Load(@"..\..\..\Catalogue.xml");
            var albumNodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode albumNode in albumNodes)
            {
                Console.WriteLine(albumNode["catalogue:name"].InnerText);
            }
        }
    }
}
