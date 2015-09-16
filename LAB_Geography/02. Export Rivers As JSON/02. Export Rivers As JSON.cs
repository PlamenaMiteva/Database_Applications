using System;
using System.Linq;
using System.Web.Script.Serialization;
using _01.Database_First;

namespace _02.Export_Rivers_As_JSON
{
    class ExportRiversAsJSON
    {
        static void Main()
        {
            var context = new GeographyEntities();
            var rivers = context.Rivers.OrderByDescending(r=>r.Length).Select(r => new
            {
                r.RiverName,
                r.Length,
                Countries = r.Countries.OrderBy(c => c.CountryName).Select(c => c.CountryName)
            });
            var jsSerializer = new JavaScriptSerializer();
            var json = jsSerializer.Serialize(rivers.ToList());
            System.IO.File.WriteAllText("../../rivers.json", json);
            //Console.WriteLine(json);
        }
    }
}
