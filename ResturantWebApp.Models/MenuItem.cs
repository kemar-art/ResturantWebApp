using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; }
        public int FoodTypeId { get; set; }

        [ForeignKey("CategoryTypeId")]
        public Category Category { get; set; }
        public int CategoryTypeId { get; set; }

    }
}
