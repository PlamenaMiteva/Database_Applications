using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DB_First;

namespace _03.Export_Internamtional_Matches
{
    class ExportInternationalMatches
    {
        static void Main()
        {
            var context = new FootballEntities();
            var result = context.InternationalMatches.OrderBy(im => im.MatchDate)
                .ThenBy(im => im.CountryHome.CountryName)
                .ThenBy(im => im.CountryAway.CountryName).
                Select(im => new
                {
                     im.HomeCountryCode,
                     im.AwayCountryCode,
                     im.HomeGoals,
                     im.AwayGoals,
                     im.MatchDate,
                     HomeCountry = im.CountryHome.CountryName,
                     AwayCountry = im.CountryAway.CountryName,
                     League = im.League.LeagueName
                }).ToList();

            XElement matches = new XElement("matches");
            foreach (var match in result)
            {
                XElement Xmatches = new XElement("match", 
                    new XElement("home-country",
                    new XAttribute("code", match.HomeCountryCode), match.HomeCountry),
                    new XElement("away-country",
                    new XAttribute("code", match.AwayCountryCode), match.AwayCountry));

                if (match.League!=null)
                {
                    Xmatches.Add(new XElement("league", match.League));
                }
                if (match.HomeGoals != null)
                {
                    Xmatches.Add(new XElement("score", match.HomeGoals + "-" +match.AwayGoals));
                }
                if (match.MatchDate != null)
                {
                    DateTime dt = match.MatchDate.Value;
                    if (dt.TimeOfDay == TimeSpan.Zero)
                    {
                        Xmatches.Add(new XAttribute("date", dt.ToString("dd-MMM-yyyy")));
                    }
                    else
                    {
                        Xmatches.Add(new XAttribute("date-time", dt.ToString("dd-MMM-yyyy hh:mm")));
                    }
                }

                matches.Add(Xmatches);
            }
            matches.Save("../../international-matches.xml");
        }
    }
}