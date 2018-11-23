using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem1.Models
{
    [Serializable]
    public class TrafficViolation
    {
        [Display(Name = "Traffic Violation #")]
        public int TrafficViolationNumber { get; set; }
        [Display(Name = "Camera #")]
        public string CameraNumber { get; set; }
        [Display(Name = "Tag #")]
        public string TagNumber { get; set; }
        [Display(Name = "Violation Type")]
        public string ViolationName { get; set; }
        [Display(Name = "Violation Date")]
        public string ViolationDate { get; set; }
        [Display(Name = "Violation Time")]
        public string ViolationTime { get; set; }
        [Display(Name = "Photo Available")]
        public string PhotoAvailable { get; set; }
        [Display(Name = "Video Available")]
        public string VideoAvailable { get; set; }
        [Display(Name = "Payment Due Date")]
        public string PaymentDueDate { get; set; }
        [Display(Name = "Payment Date")]
        public string PaymentDate { get; set; }
        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        public override bool Equals(object obj)
        {
            TrafficViolation tv = (TrafficViolation)obj;

            return tv.TrafficViolationNumber == TrafficViolationNumber ? true : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}