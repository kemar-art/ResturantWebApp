﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader? OrderHeader { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem? MenuItem { get; set; }
        [Required]
        public int MenuItemId { get; set; }

        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Name { get; set; }
    }
}
