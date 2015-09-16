using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using DB_First;

namespace _04.Import_Leagues_and_Teams_from_XML
{
    class ImportLeaguesAndTeamsFromXml
    {
        static void Main()
        {
            var context = new FootballEntities();
            var doc = XDocument.Load(@"..\\..\\leagues-and-teams.xml");
            var leagueNodes = doc.XPathSelectElements("/leagues-and-teams/league");
            int index = 1;
            foreach (var leagueNode in leagueNodes)
            {
                Console.WriteLine("Processing league #{0} ...", index++);
                League league = null;
                if (leagueNode.Element("league-name") != null)
                {
                    string leagueName = leagueNode.Element("league-name").Value;
                    if (context.Leagues.Any(l => l.LeagueName == leagueName))
                    {
                        Console.WriteLine("Existing league: {0}", leagueName);
                        league = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);
                    }
                    else
                    {
                        league = (new League()
                        {
                            LeagueName = leagueName
                        });
                        context.Leagues.Add(league);
                        Console.WriteLine("Creating league: {0}", leagueName);
                    }
                }
                var teamNodes = leagueNode.XPathSelectElements("teams/team");
                if (teamNodes.Count() != 0)
                {
                    foreach (var teamNode in teamNodes)
                    {
                        var teamNames = teamNode.Attributes("name").Select(t => t.Value);
                        string teamName = teamNames.FirstOrDefault().ToString();
                        string countryName = "";
                        string code = null;
                        if (teamNode.Attributes("country").Any())
                        {
                            var countryNames = teamNode.Attributes("country").Select(t => t.Value);
                            countryName = countryNames.FirstOrDefault().ToString();
                            var codes =
                                context.Countries.Where(c => c.CountryName == countryName).Select(c => c.CountryCode);
                            code = codes.FirstOrDefault().ToString();
                        }
                        Team team = null;
                        var query =
                            context.Teams.Select(t => t.TeamName == teamName && t.Country.CountryName == countryName);
                        if (query.First()!=false)
                        {
                            Console.WriteLine("Existing team: {0}", teamName);
                            team = context.Teams.FirstOrDefault(l => l.TeamName == teamName);
                            }
                        else
                        {
                            if (code != null)
                            {
                                team = new Team()
                                {
                                    TeamName = teamName,
                                    CountryCode = code
                                };
                            }
                            else
                            {
                                team = new Team()
                                {
                                    TeamName = teamName
                                }; 
                            }
                            
                            Console.WriteLine("Creating team: {0}", teamName);
                            context.Teams.Add(team);
                        }
                        if (league!=null && team!=null)
                        {
                            if (league.Teams.Contains(team))
                            {
                                Console.WriteLine("Existing team in league: {0} belongs to {1}",
                                    team.TeamName, league.LeagueName);
                            }
                            else
                            {
                                league.Teams.Add(team);
                                Console.WriteLine("Added team to league: {0} to league {1}",
                                    team.TeamName, league.LeagueName);
                            }
                        }
                        
                    }
                }
            }
            context.SaveChanges();
        }
    }
}

