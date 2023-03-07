using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWeb.ViewModel
{
    public class Cart
    {
        public int RoomId { get; set; }
      
        [Required(ErrorMessage = "Image  can not be empty")]
        public string Image { get; set; }
    
        [Required(ErrorMessage = "Amount can not be empty")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Wrong format for Amount number")]
        public decimal? Amount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Title { get; set; }
    }
}
