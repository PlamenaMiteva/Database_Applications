using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using DB_First;

namespace _05.generate_Random_Mtaches
{
    class GenerateRandomMatches
    {
        static void Main()
        {
            var context = new FootballEntities();
            var doc = XDocument.Load(@"..\\..\\generate-matches.xml");
            var generateNodes = doc.XPathSelectElements("/generate-random-matches/generate");
            var index = 1;
            foreach (var generateNode in generateNodes)
            {
                int count = 10;
                int maxGoals = 5;
                int lId = 0;
                string league = null;
                DateTime startDate = new DateTime(2000, 1, 1);
                DateTime endDate = new DateTime(2015, 12, 31);
                Console.WriteLine("Processing request #{0} ...", index++);
                if (generateNode.Attributes("generate-count").Any())
                {
                    var generateCounts = generateNode.Attributes("generate-count").Select(t => t.Value);
                    count = int.Parse(generateCounts.FirstOrDefault().ToString());
                }
                if (generateNode.Attributes("max-goals").Any())
                {
                    var goals = generateNode.Attributes("max-goals").Select(t => t.Value);
                    maxGoals = int.Parse(goals.FirstOrDefault().ToString());
                }
                if (generateNode.Element("league") != null)
                {
                    league = generateNode.Element("league").Value;
                    var leagueId = context.Leagues.Where(l => l.LeagueName == league).Select(l => l.Id).First();
                    lId = int.Parse(leagueId.ToString());
                }
                if (generateNode.Element("start-date") != null)
                {
                    string date = generateNode.Element("start-date").Value;
                    startDate = Convert.ToDateTime(date);
                }
                if (generateNode.Element("end-date") != null)
                {
                    string enddate = generateNode.Element("end-date").Value;
                    endDate = Convert.ToDateTime(enddate);
                }
                for (int i = 0; i < count; i++)
                {
                    TimeSpan timeSpan = endDate - startDate;
                    var randomDate = new Random();
                    TimeSpan newSpan = new TimeSpan(0, randomDate.Next(0, (int)timeSpan.TotalMinutes), 0);
                    DateTime newMatchDate = startDate + newSpan;
                    string homeTeam = null;
                    string awayTeam = null;
                    int homeTeamId = 0;
                    int awayTeamId = 0;
                    if (league != null)
                    {
                        var leagueQuery = context.Leagues.Where(l => l.LeagueName == league)
                            .Select(l => l.Teams.Select(t => t.TeamName)).ToList();
                        var list = leagueQuery[0].ToList();
                        while (awayTeam == homeTeam)
                        {
                            Random rnd = new Random();
                            int inx1 = rnd.Next(list.Count());
                            homeTeam = list[inx1];
                            int inx2 = rnd.Next(list.Count());
                            awayTeam = list[inx2];
                        }
                        var homeId = context.Teams.Where(t => t.TeamName == homeTeam).Select(t => t.Id).First();
                        homeTeamId = int.Parse(homeId.ToString());
                        var awayId = context.Teams.Where(t => t.TeamName == awayTeam).Select(t => t.Id).First();
                        awayTeamId = int.Parse(awayId.ToString());
                    }
                    else
                    {
                        var teamQuery = context.Teams
                            .Select(t => t.TeamName);
                        var list = teamQuery.ToList();
                        while (awayTeam == homeTeam)
                        {
                            Random rnd = new Random();
                            int inx1 = rnd.Next(list.Count());
                            homeTeam = list[inx1];
                            int inx2 = rnd.Next(list.Count());
                            awayTeam = list[inx2];
                        }
                        var homeId = context.Teams.Where(t => t.TeamName == homeTeam).Select(t => t.Id).First();
                        homeTeamId = int.Parse(homeId.ToString());
                        var awayId = context.Teams.Where(t => t.TeamName == awayTeam).Select(t => t.Id).First();
                        awayTeamId = int.Parse(awayId.ToString());
                    }
                    Random rand = new Random();
                    int homeTeamGoals = rand.Next(0, maxGoals);
                    int awayTeamGoals = rand.Next(0, maxGoals);
                    if (lId != 0)
                    {
                        Console.WriteLine("{0}: {1} - {2}: {3}-{4} ({5})",
                            newMatchDate.ToString("dd/MMM/yyyy"),
                            homeTeam,awayTeam,homeTeamGoals,awayTeamGoals,league);
                        context.TeamMatches.Add(new TeamMatch()
                        {
                            HomeTeamId = homeTeamId,
                            AwayTeamId = awayTeamId,
                            HomeGoals = homeTeamGoals,
                            AwayGoals = awayTeamGoals,
                            MatchDate = newMatchDate,
                            LeagueId = lId
                        });
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1} - {2}: {3}-{4} (no leaugue)",
                            newMatchDate.ToString("dd/MMM/yyyy"),
                            homeTeam, awayTeam, homeTeamGoals, awayTeamGoals);
                        context.TeamMatches.Add(new TeamMatch()
                        {
                            HomeTeamId = homeTeamId,
                            AwayTeamId = awayTeamId,
                            HomeGoals = homeTeamGoals,
                            AwayGoals = awayTeamGoals,
                            MatchDate = newMatchDate
                            });
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
