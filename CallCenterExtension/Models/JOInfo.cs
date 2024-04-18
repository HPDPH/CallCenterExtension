using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenterExtension.Models
{
    public class JOInfo
    {
        public string JODATE { get; set; }
        public string JONO { get; set; }
        public string MEDREPNAME { get; set; }
        public string PATIENTNAME { get; set; }
        public string PATIENTID { get; set; }
        public string TrayNo { get; set; }
        public string CheckStart { get; set; }
        public string CheckEnd { get; set; }
        public string SUPPLIES_REMARKS { get; set; }
        public string CHECKDT { get; set; }
        public string CHECKBY { get; set; }
        public string VERIFIEDBY { get; set; }
        public string VERIFIEDDT { get; set; }
        public string STAT_DESC { get; set; }
        public string REMARKS { get; set; }
    }
}