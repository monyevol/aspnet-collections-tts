using System;
using System.ComponentModel.DataAnnotations;

namespace TrafficTicketsSystem1.Models
{
    [Serializable]
    public class Camera
    {
        [Display(Name = "Camera #")]
        public string CameraNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }

        public override bool Equals(object obj)
        {
            Camera cmr = (Camera)obj;

            if (cmr.CameraNumber == CameraNumber)
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