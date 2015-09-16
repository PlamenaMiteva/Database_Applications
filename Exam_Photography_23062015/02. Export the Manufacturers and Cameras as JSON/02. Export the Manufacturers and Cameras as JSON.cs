using System;
using System.Linq;
using _01.Database_First;
using System.Web.Script.Serialization;

namespace _02.Export_the_Manufacturers_and_Cameras_as_JSON
{
    class ExportManufacturersCamerasJson
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var manifacturers = context.Manufacturers.OrderBy(m => m.Name).Select(m => new
            {
                m.Name,
                Cameras = context.Cameras.Where(c=>c.ManufacturerId==m.Id).OrderBy(c => c.Model).Select(c => new
                {
                    c.Model,
                    c.Price
                })
            }).ToList();
            var ser = new JavaScriptSerializer();
            var json = ser.Serialize(manifacturers);
            System.IO.File.WriteAllText("../../manufactureres-and-cameras.json", json);
        }
    }
}
