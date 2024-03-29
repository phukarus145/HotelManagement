﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
    public partial class BookRoom
    {
        public BookRoom()
        {
            RoomInBooking = new HashSet<RoomInBooking>();
            ServiceInRoom = new HashSet<ServiceInRoom>();
        }

        [Required(ErrorMessage = "Id can not be empty")]
        public int Id { get; set; }
        public string CouponId { get; set; }
        public int? AccountId { get; set; }

        [Required(ErrorMessage = "StarTime can not be empty")]
        [DataType(DataType.Date)]
        [Display(Name = "CheckIn")]
        public DateTime? StarTime { get; set; }

        [Required(ErrorMessage = "EndTime can not be empty")]
        [DataType(DataType.Date)]
        [Display(Name = "CheckOut")]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "Cmnd can not be empty")]
        [RegularExpression("[0-9]{9,12}", ErrorMessage = "Wrong format for Cmnd number")]
        public decimal? Cmnd { get; set; }
        public string Status { get; set; }
        public decimal? Total { get; set; }

        public virtual Account Account { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual ICollection<RoomInBooking> RoomInBooking { get; set; }
        public virtual ICollection<ServiceInRoom> ServiceInRoom { get; set; }
    }
}