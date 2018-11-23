using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrafficTicketsSystem1.Models
{
    [Serializable]
    public class ViolationType
    {
        [Display(Name = "Violation Name")]
        public string ViolationName { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            ViolationType vt = (ViolationType)obj;

            return vt.ViolationName == ViolationName ? true : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}