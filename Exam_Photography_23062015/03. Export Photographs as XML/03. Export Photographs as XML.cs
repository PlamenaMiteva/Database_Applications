using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using _01.Database_First;

namespace _03.Export_Photographs_as_XML
{
    internal class ExportPhotographs
    {
        private static void Main()
        {
            var context = new PhotographySystemEntities();
            var photographs = context.Photographs.OrderBy(p => p.Title).Select(p => new
            {
                p.Title,
                p.Link,
                CameraModel = p.Equipment.Camera.Model,
                Megapixels = p.Equipment.Camera.Megapixels,
                LensModel = p.Equipment.Lens.Model,
                LensPrice=p.Equipment.Lens.Price,
                Category = p.Category.Name
            }).ToList();

            XElement photographsElement = new XElement("photographs");
            foreach (var photograph in photographs)
            {
                XElement xphotographs = new XElement("photograph", new XAttribute("title", photograph.Title),
                    new XElement("category", photograph.Category),
                    new XElement("link", photograph.Link),
                    new XElement("equipment",
                        new XElement("camera", photograph.CameraModel,
                            new XAttribute("megapixels", photograph.Megapixels))
                        ));
                if (photograph.LensPrice != null)
                {
                    double price =Double.Parse(photograph.LensPrice.ToString());
                    xphotographs.Add(new XElement("lens", new XAttribute("price", Math.Round(price,2).ToString("#.00")),
                        photograph.LensModel));
                }
                else
                {
                    xphotographs.Add(new XElement("lens", photograph.LensModel)); 
                }
                photographsElement.Add(xphotographs);
            }
            photographsElement.Save("../../photographs.xml");
        }
    }
}