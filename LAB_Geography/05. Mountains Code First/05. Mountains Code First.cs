using System;
using System.Data.Entity;
using System.Linq;

namespace _05.Mountains_Code_First
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MountainsMigrationStarategy());

            Country c = new Country()
            {
                Code = "CH",
                Name = "Switzerland"
            };
            Mountain m = new Mountain()
            {
                Name = "Stara planina"
            };
            m.Peaks.Add(new Peak() { Name = "Goliam vrah", Mountain = m });
            m.Peaks.Add(new Peak() { Name = "Comm", Mountain = m });
            c.Mountains.Add(m);
            var context = new MountainsEntities();
            context.Countries.Add(c);
            context.SaveChanges();
        }


        public class MountainsMigrationStarategy : DropCreateDatabaseIfModelChanges<MountainsEntities>
        {
            protected override void Seed(MountainsEntities context)
            {
               var bulgaria = new Country()
                {
                    Code = "BG",
                    Name = "Bulgaria"
                };
                context.Countries.Add(bulgaria);
                var germany = new Country()
                {
                    Code = "DE",
                    Name = "Germany"
                };
                context.Countries.Add(germany);

                var rila = new Mountain() {Name = "Rila", Countries = {bulgaria}};
                context.Mountains.Add(rila);
                var pirin = new Mountain() { Name = "Pirin", Countries = { bulgaria } };
                context.Mountains.Add(pirin);
                var rhodopes = new Mountain() { Name = "Rhodopes", Countries = { bulgaria } };
                context.Mountains.Add(rhodopes);

                var musala = new Peak() { Name = "Musala", Elevation = 2925, Mountain = rila};
                context.Peaks.Add(musala);
                var malyovitsa = new Peak() { Name = "Malyovitsa", Elevation = 2729, Mountain = rila};
                context.Peaks.Add(malyovitsa);
                var vihren = new Peak() { Name = "Vihren", Elevation = 2914, Mountain = pirin };
                context.Peaks.Add(vihren);
            }
        }
    }
}
