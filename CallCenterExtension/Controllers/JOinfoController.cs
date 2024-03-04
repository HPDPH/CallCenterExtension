using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterExtension.Models;

namespace CallCenterExtension.Controllers
{
    public class JOinfoController : Controller
    {
        private string CALLCENTER = ConfigurationManager.ConnectionStrings["CALLCENTER"].ConnectionString;
        private string ORDER = ConfigurationManager.ConnectionStrings["ORDER"].ConnectionString;
        string str = "";
        // GET: JOinfo
        public ActionResult Index()
        {
            // Retrieve the cookie
            HttpCookie cookie = Request.Cookies["CallCenter"];

            // Retrieve values from the cookie
            ViewBag.EmpName = cookie["EmpName"];
            ViewBag.Dept = cookie["Dept"];
            ViewBag.UserRole = cookie["UserRole"];
            ViewBag.EmpID = cookie["EmpID"];

            // Check if the cookie exists and has values
            if (cookie == null || string.IsNullOrEmpty(cookie["EmpID"]))
            {
                return RedirectToAction("Index", "Login");
            }

            List<JOInfo> Display = new List<JOInfo>();
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CALLCENTER))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                string str = "SELECT TOP 10 * FROM JOInfoTest";
                SqlCommand cmd = new SqlCommand(str, cn);
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);

                while (dr.Read())

                {
                    JOInfo JOTable = new JOInfo
                    {
                        JODate = dr["JODate"].ToString(),
                        MedName = dr["MedName"].ToString(),
                        PName = dr["PName"].ToString(),
                        JONum = dr["JONum"].ToString(),
                        TrayNum = dr["TrayNum"].ToString(),
                        SChecking = dr["SChecking"].ToString(),
                        EChecking = dr["EChecking"].ToString(),
                        ChkName = dr["ChkName"].ToString(),
                        NoSupRec = dr["NoSupRec"].ToString()
                    };
                    Display.Add(JOTable);

                }
            }
            ViewBag.Display = Display;

            return View();

        }

        public ActionResult InsertJO(OrderLab orderLab)
        {
            using (SqlConnection cn = new SqlConnection(ORDER))
            {

                if (cn.State == ConnectionState.Closed) cn.Open();

                str = "INSERT INTO order_lab (TrxNo,docdate,patientID,TrayNo,SuppliesQty,CheckStart,CheckEnd,VerifiedBy,VerifiedDt,CheckBy,CheckDt,Stat) " +
                   "  VALUES (@TrxNo,@docdate,@patientID,@TrayNo,@SuppliesQty,@CheckStart,@CheckEnd,@VerifiedBy,@VerifiedDt,@CheckBy,@CheckDt,@Stat)";
                SqlCommand cmd = new SqlCommand(str, cn);
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@TrxNo", orderLab.TrxNo);
                cmd.Parameters.AddWithValue("@docdate", orderLab.docdate);
                cmd.Parameters.AddWithValue("@patientID", orderLab.patientID);
                cmd.Parameters.AddWithValue("@TrayNo", orderLab.TrayNo);
                cmd.Parameters.AddWithValue("@SuppliesQty", orderLab.SuppliesQty);
                cmd.Parameters.AddWithValue("@CheckStart", orderLab.CheckStart);
                cmd.Parameters.AddWithValue("@CheckEnd", orderLab.CheckEnd);
                cmd.Parameters.AddWithValue("@VerifiedBy", orderLab.VerifiedBy);
                cmd.Parameters.AddWithValue("@VerifiedDt", orderLab.VerifiedDt);
                cmd.Parameters.AddWithValue("@CheckBy", orderLab.CheckBy);
                cmd.Parameters.AddWithValue("@CheckDt", orderLab.CheckDt);
                cmd.Parameters.AddWithValue("@Stat", '1');
                //cmd.Parameters.AddWithValue("@For_dispute_remarks", orderLab.For_dispute_remarks);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();
                return Json("Successful", JsonRequestBehavior.AllowGet);
                

            }


        }


    }
}