using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenterExtension.Models
{
    public class JOInfo
    {
        public string JODate { get; set; }
        public string MedName { get; set; }
        public string PName { get; set; }
        public string JONum { get; set; }
        public string TrayNum { get; set; }
        public string SChecking { get; set; }
        public string EChecking { get; set; }
        public string ChkName { get; set; }
        public string NoSupRec { get; set; }

    }
}