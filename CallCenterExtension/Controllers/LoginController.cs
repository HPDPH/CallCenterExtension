using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CallCenterExtension.Models;
using Newtonsoft.Json;
using System.Net;

namespace CallCenterExtension.Controllers
{
    public class LoginController : Controller
    {
        private string HPCOMMON = ConfigurationManager.ConnectionStrings["HPCOMMON"].ConnectionString;

        private string HPCOMMONTEST = ConfigurationManager.ConnectionStrings["HPCOMMONTEST"].ConnectionString;

        // GET: Login
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            // Check if the cookie exists and has values
            if (cookie != null)
            {
                return RedirectToAction("Index", "JOinfo");
            }
            return View();
        }

        //POST:
        [HttpPost]
        public ActionResult Index(Userlogin user)
        {
            if (user.UserID != null && user.Password != null)
            {
                List<Userlogin> Display = new List<Userlogin>();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                using (SqlConnection cn = new SqlConnection(HPCOMMON))
                using (SqlConnection cn1 = new SqlConnection(HPCOMMONTEST))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                                
                    string str = "HPCOMMON.dbo.UserLogin  @user, @pass";
                    SqlCommand cmd = new SqlCommand(str, cn);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@user", user.UserID);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        if (cn1.State == ConnectionState.Closed) cn1.Open();

                        // Get UserID from User table
                        int userID = Convert.ToInt32(dt.Rows[0]["UserID"]);

                        // Check if the user exists in CCUser table
                        string str1 = "SELECT * FROM CCUser WHERE EmpID = @UserID";
                        SqlCommand cmd1 = new SqlCommand(str1, cn1);
                        cmd1.CommandTimeout = 0;
                        cmd1.Parameters.AddWithValue("@UserID", userID); // Use the retrieved UserID as parameter value
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        dt1.Load(dr1);

                        int empID = Convert.ToInt32(dt1.Rows[0]["EmpID"]);

                        if (empID == userID)
                        {

                            DataRow row = dt1.Rows[0];

                            HttpCookie cookie = new HttpCookie("CallCenter");
                            //cookie.Values.Add("UserID", Convert.ToString(row["UserID"]));
                            cookie.Values.Add("EmpName", Convert.ToString(row["EmpName"]));
                            cookie.Values.Add("Dept", Convert.ToString(row["Dept"]));
                            cookie.Values.Add("UserRole", Convert.ToString(row["UserRole"]));
                            cookie.Values.Add("EmpID", Convert.ToString(row["EmpID"]));

                            cookie.Expires = DateTime.Now.AddDays(1);

                            Response.Cookies.Add(cookie);

                        }


                        return RedirectToAction("Index", "JOinfo");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }

                }
            }
            return View();
        }

        public ActionResult LogOut() {

            // Check if the cookie exists
            if (Request.Cookies["CallCenter"] != null)
            {
                // Retrieve the cookie
                HttpCookie cookie = Request.Cookies["CallCenter"];

                // Expire the cookie by setting its expiration date to a past date
                cookie.Expires = DateTime.Now.AddDays(-1);

                // Add the modified cookie to the response
                Response.Cookies.Add(cookie);
            }

            // Redirect to the Index action of the Login controller
            return RedirectToAction("Index", "Login");

        }
    }
}