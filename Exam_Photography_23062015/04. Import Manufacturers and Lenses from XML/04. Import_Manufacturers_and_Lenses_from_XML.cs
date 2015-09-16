using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using _01.Database_First;

namespace _04.Import_Manufacturers_and_Lenses_from_XML
{
    class ImportManufacturersLensesXml
    {
        private static void Main()
        {
            var context = new PhotographySystemEntities();
            var doc = XDocument.Load(@"..\\..\\manufacturers-and-lenses.xml");
            var photographyNodes = doc.XPathSelectElements("/manufacturers-and-lenses/manufacturer");
            int index = 1;
            foreach (var photographyNode in photographyNodes)
            {
                Console.WriteLine("Processing manufacturer #{0} ...", index++);
                string manifacturer = photographyNode.Element("manufacturer-name").Value;
                var query = context.Manufacturers.Where(m=>m.Name==manifacturer).Select(m => m.Name);
                if (!query.Any())
                {
                    context.Manufacturers.Add(new Manufacturer()
                    {
                        Name = manifacturer
                    });
                    context.SaveChanges();
                    Console.WriteLine("Created manufacturer: {0}", manifacturer);
                }
                else
                {
                    Console.WriteLine("Existing manufacturer: {0}", manifacturer); 
                }
                var manifacturerId = context.Manufacturers.Where(m => m.Name == manifacturer).Select(m => m.Id).FirstOrDefault();
                var lensNodes = photographyNode.XPathSelectElements("lenses/lens");
                if (lensNodes.Count()!=0)
                {
                    foreach (var lensNode in lensNodes)
                    {
                        var lensModels = lensNode.Attributes("model").Select(l => l.Value);
                        string model = lensModels.FirstOrDefault().ToString();
                        var modelQuery = context.Lenses.Where(l=>l.Model==model).Select(l => l.Model);
                        if (!modelQuery.Any())
                        {
                            var lensTypes = lensNode.Attributes("type").Select(l => l.Value);
                            string type = lensTypes.FirstOrDefault().ToString();
                            if (lensNode.Attributes("price").Any())
                            {
                                var lensPrices = lensNode.Attributes("price").Select(l => l.Value);
                                decimal price = Decimal.Parse(lensPrices.FirstOrDefault().ToString());
                                context.Lenses.Add(new Lens()
                                {
                                    ManufacturerId = manifacturerId,
                                    Model = model,
                                    Type = type,
                                    Price = price
                                });
                            }
                            else
                            {
                                context.Lenses.Add(new Lens()
                                {
                                    ManufacturerId = manifacturerId,
                                    Model = model,
                                    Type = type
                                });
                            }
                            Console.WriteLine("Created lens: {0}", model);
                        }
                        else
                        {
                            Console.WriteLine("Existing lens: {0}", model);
                        }
                    }
                    
                }
            }
            context.SaveChanges();
        }
    }
}
