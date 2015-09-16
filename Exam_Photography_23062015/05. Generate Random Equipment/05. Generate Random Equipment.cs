using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using _01.Database_First;

namespace _05.Generate_Random_Equipment
{
    class GenerateRandomEquipment
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var doc = XDocument.Load(@"..\\..\\generate-equipments.xml");
            var generateNodes = doc.XPathSelectElements("/generate-random-equipments/generate");
            int index = 1;
            foreach (var generateNode in generateNodes)
            {
                int count = 1;
                string manifacturer = "Nikon";
                int mId = 11;
                Console.WriteLine("Processing request #{0} ...", index++);
                if (generateNode.Attributes("generate-count").Any())
                {
                    var counts = generateNode.Attributes("generate-count").Select(l => l.Value);
                    count = int.Parse(counts.FirstOrDefault().ToString());
                }
                for (int i = 0; i < count; i++)
                {
                    if (generateNode.Element("manufacturer")!=null)
                    {
                        manifacturer = generateNode.Element("manufacturer").Value.ToString();
                        var manifacturers = context.Manufacturers.Where(m => m.Name == manifacturer).Select(m => m.Name);
                        if (!manifacturers.Any())
                        {
                            context.Manufacturers.Add(new Manufacturer()
                            {
                                Name = manifacturer
                            });
                        }
                        var query = context.Manufacturers.Where(m => m.Name == manifacturer).Select(m => m.Id);
                        mId = query.FirstOrDefault();
                    }
                    var cameras = context.Cameras.ToList();
                    Random rnd = new Random();
                    int inx = rnd.Next(cameras.Count());
                    Camera newCamera = cameras[inx];
                    newCamera.ManufacturerId = mId;

                    var lenses = context.Lenses.ToList();
                    Random r = new Random();
                    int inx2 = r.Next(lenses.Count());
                    Lens newLens = lenses[inx2];
                    newLens.ManufacturerId = mId;
                    context.Lenses.Add(newLens);
                    context.SaveChanges();

                    var lensID = context.Lenses.Where(l=>l.Model==newLens.Model).Select(l => l.Id).FirstOrDefault();
                    var cameraID = context.Cameras.Where(c => c.Model == newCamera.Model).Select(c => c.Id).FirstOrDefault();
                    context.Equipments.Add(new Equipment()
                    {
                        LensId = lensID,
                        CameraId = cameraID
                    });
                    Console.WriteLine("Equipment added: {0} (Camera: {1} - Lens: {2})",
                        manifacturer,
                        newCamera.Model,
                        newLens.Model);
                    context.SaveChanges();
                    }
            }
        }
    }
}
