﻿using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.Models.ViewModel
{
	public class OrderDetailsVM
	{
		public OrderHeader OrderHeader { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}
