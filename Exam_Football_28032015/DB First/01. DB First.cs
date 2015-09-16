using System;
using System.Linq;

namespace DB_First
{
    class DBFirst
    {
        static void Main()
        {
            var context = new FootballEntities();
            var teams = context.Teams.Select(t => t.TeamName);
            foreach (var team in teams)
            {
                Console.WriteLine(team);
            }
        }
    }
}
