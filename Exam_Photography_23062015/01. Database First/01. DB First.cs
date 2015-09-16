using System;
using System.Linq;

namespace _01.Database_First
{
    class DatabaseFirst
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var manifacturers = context.Manufacturers.OrderBy(m=>m.Name).Select(m => new
            {
                m.Name,
                Cameras= context.Cameras.Where(c=>c.ManufacturerId==m.Id).OrderBy(c=>c.Model).Select(c=>c.Model)
            });
            foreach (var manifacturer in manifacturers)
            {
                Console.WriteLine("Company: {0}", manifacturer.Name);
                foreach (var camera in manifacturer.Cameras)
                {
                    Console.Write(camera+"'");
                }
                Console.WriteLine();
            }
        }
    }
}
