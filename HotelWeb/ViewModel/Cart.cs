using HotelWeb.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWeb.ViewModel
{
    public class Cart
    {
        private static string today { get
            {
                return DateTime.Now.ToString();
            } }
        public int RoomId { get; set; }
      
        [Required(ErrorMessage = "Image can not be empty")]
        [ProductImageValidation]
        public string Image { get; set; }
    
        [Required(ErrorMessage = "Amount can not be empty")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Wrong format for Amount number")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = "CheckIn can not be empty")]
        [DataType(DataType.Date)]
        [DateValidation(CompareToOperation.LessThan, "CheckIn", "CheckOut",ErrorMessage = "CheckIn must not be Greater than CheckOut")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "CheckOut can not be empty")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public string Title { get; set; }
    }
}
