using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

namespace _01.Show_Data_from_Related_Tables
{
    class AllProblems
    {
        static void Main()
        {
            var context = new AdsEntities();


            //Problem 1. Show Data from Related Tables


            //Without INCLUDE

            //var ads = context.Ads;
            //foreach (var ad in ads)
            //{
            //    if (ad.TownId == null || ad.CategoryId == null)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} {1} {2} {3} {4}",
            //                ad.Title,
            //                ad.AdStatus.Status,
            //                ad.Category.Name,
            //                ad.Town.Name,
            //                ad.AspNetUser.Name
            //                );
            //    }
            //}


            //Using INCLUDE


            //var ads = context.Ads
            //    .Include(ad => ad.Category)
            //    .Include(ad => ad.AdStatus)
            //    .Include(ad => ad.Town)
            //    .Include(ad => ad.AspNetUser);

            //foreach (var ad in ads)
            //{
            //    if (ad.TownId == null || ad.CategoryId == null)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} {1} {2} {3} {4}",
            //                ad.Title,
            //                ad.AdStatus.Status,
            //                ad.Category.Name,
            //                ad.Town.Name,
            //                ad.AspNetUser.Name
            //                );
            //    }
            //}




            //Problem 2. Play with ToList()



            //Non-optimized
            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //var ads = context.Ads
            //    .ToList()
            //    .Where(ad=>ad.AdStatus.Status=="Published")
            //    .Select(ad => new
            //{
            //    ad.Title,
            //    ad.Date,
            //    Category = ad.Category.Name,
            //    Town = ad.Town.Name
            //}).ToList().OrderBy(ad => ad.Date);

            //stopwatch.Stop();
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);


            //Optimized
            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //var ads = context.Ads
            //    .Where(ad=>ad.AdStatus.Status=="Published").OrderBy(ad => ad.Date)
            //    .Select(ad => new
            //{
            //    ad.Title,
            //    ad.Date,
            //    Category = ad.Category.Name,
            //    Town = ad.Town.Name
            //}).ToList();

            //stopwatch.Stop();
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);

            //Problem 3. Select Everything vs. Select Certain Columns

            //Non-optimized
            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //var ads = context.Ads;
            //foreach (var ad in ads)
            //{
            //    Console.WriteLine(ad.Title);
            //}
            //stopwatch.Stop();
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);


            //Optimized
            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //var ads = context.Ads.Select(ad => ad.Title);
            //foreach (var ad in ads)
            //{
            //    Console.WriteLine(ad);
            //}
            //stopwatch.Stop();
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);
        }
    }
}
