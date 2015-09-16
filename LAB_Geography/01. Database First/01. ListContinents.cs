using System;
using System.Linq;

namespace _01.Database_First
{
    class ListContinents
    {
        static void Main()
        {
            var context = new GeographyEntities();
            var continents = context.Continents.Select(c => c.ContinentName);
            foreach (var continent in continents)
            {
                Console.WriteLine(continent);
            }
        }
    }
}
