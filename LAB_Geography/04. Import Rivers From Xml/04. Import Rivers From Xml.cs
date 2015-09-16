using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using _01.Database_First;

namespace _04.Import_Rivers_From_Xml
{
    internal class ImportRiversFromXml
    {
        private static void Main()
        {
            var context = new GeographyEntities();
            var doc = XDocument.Load(@"..\\..\\rivers.xml");
            var riverNodes = doc.XPathSelectElements("/rivers/river");
            foreach (var riverNode in riverNodes)
            {
                string riverName = riverNode.Element("name").Value;
                int riverLength = int.Parse(riverNode.Element("length").Value);
                string riverOutflow = riverNode.Element("outflow").Value;
                var river = new River()
                {
                    RiverName = riverName,
                    Length = riverLength,
                    Outflow = riverOutflow
                };
                try
                {
                    int riverDrainageArea = int.Parse(riverNode.Element("drainage-area").Value);
                    int riverAverageDischarge = int.Parse(riverNode.Element("average-discharge").Value);
                    river.AverageDischarge = riverAverageDischarge;
                    river.DrainageArea = riverDrainageArea;
                }
                catch (NullReferenceException nullRefEx)
                {

                }
                var countryNodes = riverNode.XPathSelectElements("countries/country");
                var countryNames = countryNodes.Select(c => c.Value);
                foreach (var countryName in countryNames)
                {
                    var country = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                    river.Countries.Add(country);
                }
                context.Rivers.Add(river);
                context.SaveChanges();
            }
        }
    }
}
