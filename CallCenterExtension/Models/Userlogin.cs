using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenterExtension.Models
{
    public class Userlogin
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        public string EmpName { get; set; }
        public string Dept { get; set; }

    }

    public class User
    {
        public string EmpName { get; set; }
        public string Dept { get; set; }
        public string UserRole { get; set; }
        public string EmpID { get; set; }
    }

    public class MedTech
    {
        public string MedRepID { get; set; }
        public string MedRepName { get; set; }
        public string HSSched { get; set; }
    }

}