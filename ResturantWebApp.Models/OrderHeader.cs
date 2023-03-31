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
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        public string? UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        [Required]
        [Display(Name = "Pick Up Date")]
        [NotMapped]
        public DateTime PickUpDate { get; set; } 

        public string? OrderStatus { get; set; }

        public string? Comments { get; set;}

        public string? TransactionId { get; set; }

        [Required]
        [Display(Name = "Pickup Name")]
        public string? PickupName { get; set;}

		[Required]
		[Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}