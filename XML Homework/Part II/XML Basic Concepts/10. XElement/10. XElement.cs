using System.Xml.Linq;

internal class CreatingDocumentLINQToXML
{
    private static void Main()
    {

        var dirs = new XElement("root-dir", new XAttribute("path", "C:\\Example"),
            new XElement("dir", new XAttribute("name", "docs"),
                new XElement("file", new XAttribute("name", "tutorial.pdf")),
                new XElement("file", new XAttribute("name", "TODO.txt")),
                new XElement("file", new XAttribute("name", "Presentation.pptx"))),
            new XElement("dir", new XAttribute("name", "photos"),
                new XElement("file", new XAttribute("name", "friends.jpg")),
                new XElement("file", new XAttribute("name", "the_cake.jpg")),
                new XElement("file", new XAttribute("name", "baloons.jpg"))),
            new XElement("dir", new XAttribute("name", "travel"),
                new XElement("file", new XAttribute("name", "IMG24152.jpg"))));

        dirs.Save("../../directories.xml");
    }
}
    




