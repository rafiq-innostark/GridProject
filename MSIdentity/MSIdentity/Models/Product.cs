using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSIdentity.Models
{
    public class Product
    {

        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public Category Category { get; set; }
    }
}