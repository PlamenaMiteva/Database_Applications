using System;
using System.ComponentModel.DataAnnotations;

namespace NewsModels
{
    public class News
    {
        public int Id { get; set; }

        [ConcurrencyCheck] 
        public string Content { get; set; }
    }
}
