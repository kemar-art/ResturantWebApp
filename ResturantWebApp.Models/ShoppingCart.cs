using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
	public class ShoppingCart
	{
        public int Id { get; set; }

		
		[ForeignKey("MenuItemId")]
		[ValidateNever]
		public MenuItem? MenuItem { get; set; }
		public int MenuItemId { get; set; }

		[Range(1, 100, ErrorMessage = "Please select a count from 1 to 100")]
		public int Count { get; set; }

		
		[ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
		public string? ApplicationUserId { get; set; }

	}
}
