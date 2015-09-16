using System;
using System.Linq;
using System.Web.Script.Serialization;
using DB_First;

namespace _02.Export_the_Leagues_and_Teams_as_JSON
{
    class Program
    {
        static void Main()
        {
            var context = new FootballEntities();
            var leagues = context.Leagues.OrderBy(l=>l.LeagueName).Select(l => new
            {
                leagueName = l.LeagueName,
                teams = l.Teams.OrderBy(t => t.TeamName).Select(t => t.TeamName)
            }).ToList();

            var ser = new JavaScriptSerializer();
            var json = ser.Serialize(leagues);
            System.IO.File.WriteAllText("../../leagues-and-teams.json", json);



            //foreach (var team in teams)
            //{
            //    Console.WriteLine("League: " + team.LeagueName + " ");
            //    foreach (var t in team.Teams)
            //    {
            //        Console.WriteLine(t.TeamName);
            //    }
            //}
        }
    }
}
