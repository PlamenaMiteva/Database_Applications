namespace _05.Mountains_Code_First
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MountainsEntities : DbContext
    {
       
        public MountainsEntities()
            : base("name=MountainsEntities")
        {
        }
        public virtual DbSet<Country> Countries{ get; set; }
        public virtual DbSet<Mountain> Mountains{ get; set; }
        public virtual DbSet<Peak> Peaks { get; set; }
    }

    
}