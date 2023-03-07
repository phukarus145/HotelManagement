using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWeb.ViewModel
{
    public class ServiceCart
    {
        public int ServiceId { get; set; }

    
        public string Image { get; set; }

        
        public decimal? Amount { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
    }
}
