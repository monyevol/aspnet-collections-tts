using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem1.Models
{
    [Serializable]
    public class Vehicle
    {
        [Display(Name = "Tag #")]
        public string TagNumber { get; set; }
        [Display(Name = "Drv. Lic. #")]
        public string DrvLicNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Display(Name = "Vehicle Year")]
        public string VehicleYear { get; set; }
        public string Color { get; set; }

        public override bool Equals(object obj)
        {
            Vehicle car = (Vehicle)obj;

            if (car.TagNumber == TagNumber)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}