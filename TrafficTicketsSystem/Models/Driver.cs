using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem1.Models
{
    [Serializable]
    public class Driver
    {
        [Display(Name = "Drv. Lic. #")]
        public string DrvLicNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        [Display(Name = "ZIP Code")]
        public string ZIPCode { get; set; }

        public override bool Equals(object obj)
        {
            Driver dvr = (Driver)obj;

            return dvr.DrvLicNumber == DrvLicNumber ? true : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}