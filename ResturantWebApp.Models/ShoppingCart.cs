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

		[Display(Name = "Qty")]
		public int Count { get; set; } = 1;

		
		[ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
		public string? ApplicationUserId { get; set; }

	}
}
