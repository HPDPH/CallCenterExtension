using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenterExtension.Models
{
    public class OrderLab
    {
        public string TrxNo { get; set; } = "" ;
        public DateTime docdate { get; set; } 
        public string patientID { get; set; }
        public string TrayNo { get; set; }
        public string SuppliesQty { get; set; }
        public string CheckStart { get; set; }
        public string CheckEnd { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime VerifiedDt { get; set; }
        public string CheckBy { get; set; }
        public DateTime CheckDt { get; set; }
        public string Stat { get; set; }
        public string For_dispute_remarks { get; set; }
    }
}