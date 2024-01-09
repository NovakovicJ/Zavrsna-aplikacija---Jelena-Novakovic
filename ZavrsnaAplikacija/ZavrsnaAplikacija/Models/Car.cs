using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZavrsnaAplikacija.Models
{

    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.Rentals = new HashSet<Rental>();
        }


        public int CarId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<bool> Available { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}