using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenterExtension.Models
{
    public class OrderLab
    {
        public string HsStaff { get; set; }
        public string schedDate { get; set; }
        public string TrxNo { get; set; } 
        public string PatientID { get; set; }
        public string TrayNo { get; set; }
        public string SUPPLIES_REMARKS { get; set; }
        public string CheckStart { get; set; }
        public string CheckEnd { get; set; }
        public string VerifiedBy { get; set; }
        public string VerifiedDt { get; set; }
        public string CheckBy { get; set; }
        public string CheckDt { get; set; }
        public string Stat { get; set; }
        public string For_dispute_remarks { get; set; }
    }
}