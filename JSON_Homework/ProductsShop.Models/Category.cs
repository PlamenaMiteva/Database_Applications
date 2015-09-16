using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductsShop.Models;


namespace ProductsShop.Models
{
    public class Category
    {
        private ICollection<Product> products;
        public Category()
        {
            this.products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(3), MaxLength(15)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}

