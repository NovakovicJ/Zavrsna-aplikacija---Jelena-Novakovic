using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavrsnaAplikacija.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Driver License Number")]
        public Nullable<int> DriverLicNo { get; set; }
    }
}