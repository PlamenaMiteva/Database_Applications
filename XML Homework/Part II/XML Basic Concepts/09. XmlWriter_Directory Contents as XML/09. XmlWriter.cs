using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace _09.XmlWriter_Directory_Contents_as_XML
{
    class XmlWriterDirectoryContentsAsXML
    {
        static void Main()
        {
            List<string> docNames = new List<string> { "tutorial.pdf", "TODO.txt", "Presentation.pptx" };
            List<string> photosNames = new List<string> { "friends.jpg", "the_cake.jpg", "baloons.jpg" };
            List<string> travelNames = new List<string> { "IMG24152.jpg" };
            string fileName = "../../directories.xml";
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("root-dir");
                writer.WriteAttributeString("path", "C:\\Example");
                WriteDir(writer, "docs", docNames);
                WriteDir(writer, "photos", photosNames);
                WriteDir(writer, "travel", travelNames);
                writer.WriteEndDocument();
            }
            Console.WriteLine("Document {0} created.", fileName);
        }

        private static void WriteDir(XmlWriter writer, string dirName, List<string> names)
        {
            writer.WriteStartElement("dir");
            writer.WriteAttributeString("name", dirName);
            foreach (var name in names)
            {
                writer.WriteStartElement("file");
                writer.WriteAttributeString("name", name);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}

