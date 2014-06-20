using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Product
    {
      
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public int? Price { get; set; }
        public Category Category { get; set; }
    }
}