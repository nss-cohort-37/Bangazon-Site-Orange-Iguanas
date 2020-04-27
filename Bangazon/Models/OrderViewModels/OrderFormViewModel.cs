using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderFormViewModel
    {
        public int Units { get; set; }
        public double Cost { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<SelectListItem> OrderProducts { get; set; }

    }
}
