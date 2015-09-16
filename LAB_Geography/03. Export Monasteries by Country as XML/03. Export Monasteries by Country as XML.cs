using System;
using System.Linq;
using System.Xml.Linq;
using _01.Database_First;

namespace _03.Export_Monasteries_by_Country_as_XML
{
    internal class ExportMonasteriesByCountryAsXML
    {
        private static void Main()
        {
            var context = new GeographyEntities();
            var monasteries =
                context.Countries.Where(c => c.Monasteries.Any()).OrderBy(c => c.CountryName).Select(c => new
                {
                    c.CountryName,
                    Monasteries =
                        context.Monasteries.Where(m => m.CountryCode == c.CountryCode)
                            .OrderBy(m => m.Name)
                            .Select(m => m.Name)
                }).ToList();
            XElement XMonasteries = new XElement("monasteries");
            foreach (var country in monasteries)
            {
                XElement XCountry = new XElement("country", new XAttribute("name", country.CountryName));

                foreach (var monastery in country.Monasteries)
                {
                    XCountry.Add(new XElement("monastery", monastery));
                }
                XMonasteries.Add(XCountry);
            }
            XMonasteries.Save("../../monasteries.xml");
        }
    }
}

