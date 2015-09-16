using NewsModels;

namespace NewsSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NewsEntities : DbContext
    {
        public NewsEntities()
            : base("name=NewsEntities")
        {
        }

        public virtual DbSet<News> News { get; set; }
    }

   
}