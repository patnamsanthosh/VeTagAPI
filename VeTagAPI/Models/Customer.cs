using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeTagAPI.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsRegisterByCustomer { get; set; }
        public string EmergencyContactNumber { get; set; }
        public bool AllowCalls { get; set; }
        public string ReferenceID { get; set; }
        public string ContactOptions { get; set; }


    }
}
