﻿using System.ComponentModel.DataAnnotations;

namespace ResturantWebApp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
