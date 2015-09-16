using ProductsShop.Models;

namespace ProductsShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ShopEntities : DbContext
    {
        public ShopEntities()
            : base("name=ShopEntities")
        {
        }
         public virtual DbSet<Product> Products{ get; set; }
         public virtual DbSet<Category> Categories { get; set; }
         public virtual DbSet<User> Users { get; set; }
    }
}