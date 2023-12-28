using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZavrsnaAplikacija.Models;

namespace ZavrsnaAplikacija.Dtos
{
    public class RentalDto
    {
        public int RentalId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> CarId { get; set; }
        [Display(Name = "Date Rented")]
        public Nullable<System.DateTime> DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public Nullable<System.DateTime> DateReturned { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }
    }
}