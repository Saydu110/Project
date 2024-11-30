using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_for_Dental
{
    public class Client
    {
        public DateTime AdmissionDate { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string DoctorConsultant { get; set; }
        public string ServiceName { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string VisitType { get; set; }
        public string AdmissionDetails { get; set; }
        public decimal DiscountAmount { get; set; }
    }

}
