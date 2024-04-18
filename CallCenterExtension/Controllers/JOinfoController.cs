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

            return View();
        }
       
        public ActionResult LoadDataTable(string medRepID, string schedDate, string status, string schedDatestatus)
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            ViewBag.UserRole = cookie["UserRole"];

            // Set default value for medRepID if it's null or empty
            if (string.IsNullOrEmpty(medRepID))
            {
                medRepID = "0"; // Set default value to "0"
            }

            // Create a list to store the filtered MedTech objects
            //List<JOInfo> Display = new List<JOInfo>();
            //DataTable dt = new DataTable();

            List<JOInfo> Display = new List<JOInfo>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ORDER))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    if (ViewBag.UserRole == "Encoder")
                    {
                        string str = "SELECT distinct CAST(T.HSSCHED AS DATE) JODATE,T.TRXNO JONO,R.MEDREPNAME, RTRIM(LTRIM(P.LASTNAME + ', ' + P.FIRSTNAME + ' ' + P.MIDDLENAME)) PATIENTNAME, P.PatientID, " +
                                     "L.TrayNo,L.CheckStart,L.CheckEnd,SUPPLIES_REMARKS,CHECKDT,CHECKBY,VERIFIEDBY,VERIFIEDDT,S.STAT_DESC,For_dispute_remarks REMARKS " +
                                     "FROM ORDER_TRX T " +
                                     "INNER JOIN ORDER_PATIENT P ON P.TRXNO = T.TRXNO " +
                                     "INNER JOIN ORDER_MEDREP M ON M.TRXNO = T.TRXNO " +
                                     "LEFT JOIN MEDREP_MASTER R ON R.MEDREPID = M.MEDREPID " +
                                     "LEFT JOIN ORDER_LAB L ON L.TrxNo = T.TRXNO " +
                                     "LEFT JOIN Order_Lab_Stat S ON S.Stat_Code = L.Stat " +
                                     "WHERE R.MEDREPNAME = '" + medRepID + "' AND CAST(T.HSSCHED AS DATE) = '" + schedDate + "' AND L.STAT <> '2' ";

                        SqlCommand cmd = new SqlCommand(str, cn);
                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            JOInfo JOTable = new JOInfo
                            {
                                JODATE = dr["JODATE"].ToString(),
                                STAT_DESC = dr["STAT_DESC"].ToString(),
                                MEDREPNAME = dr["MEDREPNAME"].ToString(),
                                PATIENTNAME = dr["PATIENTNAME"].ToString(),
                                PATIENTID = dr["PATIENTID"].ToString(),
                                JONO = dr["JONO"].ToString(),
                                TrayNo = dr["TrayNo"].ToString(),
                                SUPPLIES_REMARKS = dr["SUPPLIES_REMARKS"].ToString(),
                                CheckStart = dr["CheckStart"].ToString(),
                                CheckEnd = dr["CheckEnd"].ToString(),
                                CHECKBY = dr["CHECKBY"].ToString(),
                                //CHECKDT = dr["CHECKDT"].ToString()
                            };
                            Display.Add(JOTable);
                        }
                    }
                    else if (ViewBag.UserRole == "LabManager")
                    {
                        //string str = "SELECT * FROM ORDER_LAB ORDER BY SchedDate Desc";
                        string str = "SELECT HsStaff,T.TrxNo,schedDate,PatientID,TrayNo,SUPPLIES_REMARKS,CheckStart," +
                                     "CheckEnd,CheckBy,CheckDt,Stat,For_dispute_remarks,Stat_Desc " +
                                     "FROM ORDER_LAB T LEFT JOIN Order_Lab_Stat S ON S.Stat_Code = Stat " +
                                     "where schedDate = '" + schedDatestatus + "' and Stat_Desc = '" + status + "' ";
                        SqlCommand cmd = new SqlCommand(str, cn);
                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            JOInfo JOTable = new JOInfo
                            {
                                JODATE = dr["schedDate"].ToString(),
                                STAT_DESC = dr["STAT_DESC"].ToString(),
                                MEDREPNAME = dr["hsStaff"].ToString(),
                                PATIENTID = dr["PatientID"].ToString(),
                                JONO = dr["TrxNo"].ToString(),
                                TrayNo = dr["TrayNo"].ToString(),
                                SUPPLIES_REMARKS = dr["SUPPLIES_REMARKS"].ToString(),
                                CheckStart = dr["CheckStart"].ToString(),
                                CheckEnd = dr["CheckEnd"].ToString(),
                                CHECKBY = dr["CHECKBY"].ToString(),
                                CHECKDT = dr["CHECKDT"].ToString(),
                                REMARKS = dr["For_dispute_remarks"].ToString(),
                            };
                            Display.Add(JOTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine(ex.Message);
            }

            // Return JSON result to JavaScript
            return Json(Display, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDisputeTable(string medRepID, string schedDate)
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            ViewBag.UserRole = cookie["UserRole"];

            // Set default value for medRepID if it's null or empty
            if (string.IsNullOrEmpty(medRepID))
            {
                medRepID = "0"; // Set default value to "0"
            }

            // Create a list to store the filtered MedTech objects
            //List<JOInfo> Display = new List<JOInfo>();
            //DataTable dt = new DataTable();

            List<JOInfo> Display = new List<JOInfo>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ORDER))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    if (ViewBag.UserRole == "Encoder")
                    {
                        string str = "SELECT distinct CAST(T.HSSCHED AS DATE) JODATE,T.TRXNO JONO,R.MEDREPNAME, RTRIM(LTRIM(P.LASTNAME + ', ' + P.FIRSTNAME + ' ' + P.MIDDLENAME)) PATIENTNAME, P.PatientID, " +
                                     "L.TrayNo,L.CheckStart,L.CheckEnd,SUPPLIES_REMARKS,CHECKDT,CHECKBY,VERIFIEDBY,VERIFIEDDT,S.STAT_DESC,For_dispute_remarks REMARKS " +
                                     "FROM ORDER_TRX T " +
                                     "INNER JOIN ORDER_PATIENT P ON P.TRXNO = T.TRXNO " +
                                     "INNER JOIN ORDER_MEDREP M ON M.TRXNO = T.TRXNO " +
                                     "LEFT JOIN MEDREP_MASTER R ON R.MEDREPID = M.MEDREPID " +
                                     "LEFT JOIN ORDER_LAB L ON L.TrxNo = T.TRXNO " +
                                     "LEFT JOIN Order_Lab_Stat S ON S.Stat_Code = L.Stat " +
                                     "WHERE R.MEDREPNAME = '" + medRepID + "' AND CAST(T.HSSCHED AS DATE) = '" + schedDate + "' AND L.STAT = '2' ";

                        SqlCommand cmd = new SqlCommand(str, cn);
                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            JOInfo JOTable = new JOInfo
                            {
                                JODATE = dr["JODATE"].ToString(),
                                STAT_DESC = dr["STAT_DESC"].ToString(),
                                MEDREPNAME = dr["MEDREPNAME"].ToString(),
                                PATIENTNAME = dr["PATIENTNAME"].ToString(),
                                PATIENTID = dr["PATIENTID"].ToString(),
                                JONO = dr["JONO"].ToString(),
                                TrayNo = dr["TrayNo"].ToString(),
                                SUPPLIES_REMARKS = dr["SUPPLIES_REMARKS"].ToString(),
                                CheckStart = dr["CheckStart"].ToString(),
                                CheckEnd = dr["CheckEnd"].ToString(),
                                CHECKBY = dr["CHECKBY"].ToString(),
                                REMARKS = dr["REMARKS"].ToString(),
                                //CHECKDT = dr["CHECKDT"].ToString()
                            };
                            Display.Add(JOTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine(ex.Message);
            }

            // Return JSON result to JavaScript
            return Json(Display, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertJO(OrderLab orderLab)
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            // Retrieve values from the cookie
            ViewBag.EmpName = cookie["EmpName"];
            ViewBag.EmpID = cookie["EmpID"];
            ViewBag.Dept = cookie["Dept"];

            using (SqlConnection cn = new SqlConnection(ORDER))
            {
                SqlCommand cmd;
                if (cn.State == ConnectionState.Closed) cn.Open();

                //04102024 - alalahanin kung bakit may delete dito
                //str = "delete order_lab where trxno  = '" + orderLab.TrxNo + "'";
                //cmd = new SqlCommand(str, cn);
                //cmd.ExecuteNonQuery();
                //cmd.Dispose();

                var result = "SELECT TOP 1 1 FROM order_lab WHERE trxno = '" + orderLab.TrxNo + "' and PatientID = '" + orderLab.PatientID + "' and stat = '2' ";
                cmd = new SqlCommand(result, cn);
                var exists = cmd.ExecuteScalar(); // ExecuteScalar is used to retrieve a single value (in this case, if the record exists)
                cmd.Dispose();

                if (exists == null || exists == DBNull.Value)
                {

                    str = "INSERT INTO order_lab (HsStaff,schedDate,TrxNo,PatientID,TrayNo,SUPPLIES_REMARKS,CheckStart,CheckEnd,CheckBy,CheckDt,Stat) " +
                       "  VALUES (@HsStaff,@schedDate,@TrxNo,@PatientID,@TrayNo,@SUPPLIES_REMARKS,@CheckStart,@CheckEnd,@CheckBy,@CheckDt,@Stat)";

                    cmd = new SqlCommand(str, cn);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@HsStaff", orderLab.HsStaff);
                    cmd.Parameters.AddWithValue("@schedDate", orderLab.schedDate);
                    cmd.Parameters.AddWithValue("@TrxNo", orderLab.TrxNo);
                    cmd.Parameters.AddWithValue("@PatientID", orderLab.PatientID);
                    cmd.Parameters.AddWithValue("@TrayNo", orderLab.TrayNo);
                    cmd.Parameters.AddWithValue("@SUPPLIES_REMARKS", orderLab.SUPPLIES_REMARKS);
                    cmd.Parameters.AddWithValue("@CheckStart", orderLab.CheckStart);
                    cmd.Parameters.AddWithValue("@CheckEnd", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CheckBy", ViewBag.EmpName);
                    cmd.Parameters.AddWithValue("@CheckDt", orderLab.CheckDt);
                    cmd.Parameters.AddWithValue("@Stat", orderLab.Stat);
                    //cmd.Parameters.AddWithValue("@For_dispute_remarks", orderLab.For_dispute_remarks);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //str = "INSERT INTO Order_Lab_Stat(Stat_Code, Stat_Desc, TrxNo, ModBy, ModEmpID ,ModDept) " +
                    //   "  VALUES (@Stat_Code,@Stat_Desc,@TrxNo,@ModBy,@ModEmpID,@ModDept)"; ;
                    //SqlCommand cmd1 = new SqlCommand(str, cn);
                    //cmd1.CommandTimeout = 0;
                    //cmd1.Parameters.AddWithValue("@Stat_Code", orderLab.Stat);
                    //cmd1.Parameters.AddWithValue("@Stat_Desc", orderLab.For_dispute_remarks);
                    //cmd1.Parameters.AddWithValue("@TrxNo", orderLab.TrxNo);
                    //cmd1.Parameters.AddWithValue("@ModBy", ViewBag.EmpName);
                    //cmd1.Parameters.AddWithValue("@ModEmpID", ViewBag.EmpID);
                    //cmd1.Parameters.AddWithValue("@ModDept", ViewBag.Dept);
                    //cmd1.ExecuteNonQuery();


                    //cmd1.Dispose();

                }
                else
                {
                    //str = "UPDATE order_lab SET VerifiedBy = @VerifiedBy , VerifiedDt = @VerifiedDt, Stat = @Stat, For_dispute_remarks = @For_dispute_remarks" +
                    //   "  WHERE PatientID = @patientID";

                    str = " UPDATE order_lab SET HsStaff = '" + orderLab.HsStaff + "' , schedDate = '" + orderLab.schedDate + "', TrxNo = '" + orderLab.TrxNo + "', PatientID = '" + orderLab.PatientID + "'," +
                          " TrayNo = '" + orderLab.TrayNo + "' , SUPPLIES_REMARKS = '" + orderLab.SUPPLIES_REMARKS + "', CheckStart = '" + orderLab.CheckStart + "', CheckEnd = '" + DateTime.Now + "'," +
                          " CheckBy = '" + ViewBag.EmpName + "' , CheckDt = '" + orderLab.CheckDt + "', Stat = '4' , For_Dispute_remarks = '" + orderLab.For_dispute_remarks + "'" +
                          "  WHERE PatientID = '" + orderLab.PatientID + "' and TrxNo = '" + orderLab.TrxNo + "' and Stat = '2' ";


                    cmd = new SqlCommand(str, cn);
                    cmd.CommandTimeout = 0;
                    //cmd.Parameters.AddWithValue("@patientID", VerifyOrderLab.PatientID);
                    //cmd.Parameters.AddWithValue("@VerifiedBy", ViewBag.EmpName);
                    //cmd.Parameters.AddWithValue("@VerifiedDt", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@Stat", VerifyOrderLab.Stat);
                    //cmd.Parameters.AddWithValue("@For_dispute_remarks", VerifyOrderLab.For_dispute_remarks);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                }
                cn.Close();

                return Json("Done", JsonRequestBehavior.AllowGet);


            }


        }


        public ActionResult VerifyJO(OrderLab VerifyOrderLab)
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            // Retrieve values from the cookie
            ViewBag.EmpName = cookie["EmpName"];
            ViewBag.EmpID = cookie["EmpID"];
            ViewBag.Dept = cookie["Dept"];

            using (SqlConnection cn = new SqlConnection(ORDER))
            {

                if (cn.State == ConnectionState.Closed) cn.Open();

                str = "UPDATE order_lab SET VerifiedBy = @VerifiedBy , VerifiedDt = @VerifiedDt, Stat = @Stat , For_dispute_remarks = '' " +
                   "  WHERE TrxNo = @TrxNo";


                SqlCommand cmd = new SqlCommand(str, cn);
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@TrxNo", VerifyOrderLab.TrxNo);
                cmd.Parameters.AddWithValue("@VerifiedBy", ViewBag.EmpName);
                cmd.Parameters.AddWithValue("@VerifiedDt", DateTime.Now);
                cmd.Parameters.AddWithValue("@Stat", VerifyOrderLab.Stat);
                //cmd.Parameters.AddWithValue("@For_dispute_remarks", VerifyOrderLab.For_dispute_remarks);
                cmd.ExecuteNonQuery();

                cmd.Dispose();

                //str = "INSERT INTO Order_Lab_Stat(Stat_Code, Stat_Desc, TrxNo, ModBy, ModEmpID, ModDept) " +
                //   "  VALUES (@Stat_Code,@Stat_Desc,@TrxNo,@ModBy,@ModEmpID,@ModDept)"; ;
                //SqlCommand cmd1 = new SqlCommand(str, cn);
                //cmd1.CommandTimeout = 0;
                //cmd1.Parameters.AddWithValue("@Stat_Code", VerifyOrderLab.Stat);
                //cmd1.Parameters.AddWithValue("@Stat_Desc", VerifyOrderLab.For_dispute_remarks);
                //cmd1.Parameters.AddWithValue("@TrxNo", VerifyOrderLab.TrxNo);
                //cmd1.Parameters.AddWithValue("@ModBy", ViewBag.EmpName);
                //cmd1.Parameters.AddWithValue("@ModEmpID", ViewBag.EmpID);
                //cmd1.Parameters.AddWithValue("@ModDept", ViewBag.Dept);
                //cmd1.ExecuteNonQuery();


                //cmd1.Dispose();

                cn.Close();


                return Json("Successful", JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult DisputeJO(OrderLab VerifyOrderLab)
        {
            HttpCookie cookie = Request.Cookies["CallCenter"];

            // Retrieve values from the cookie
            ViewBag.EmpName = cookie["EmpName"];
            ViewBag.EmpID = cookie["EmpID"];
            ViewBag.Dept = cookie["Dept"];

            using (SqlConnection cn = new SqlConnection(ORDER))
            {

                if (cn.State == ConnectionState.Closed) cn.Open();

                //str = "UPDATE order_lab SET VerifiedBy = @VerifiedBy , VerifiedDt = @VerifiedDt, Stat = @Stat, For_dispute_remarks = @For_dispute_remarks" +
                //   "  WHERE PatientID = @patientID";

                str = "UPDATE order_lab SET VerifiedBy = '" + ViewBag.EmpName + "' , VerifiedDt = '" + DateTime.Now + "', Stat = '" + VerifyOrderLab.Stat + "', For_dispute_remarks = '" + VerifyOrderLab.For_dispute_remarks + "'" +
                       "  WHERE PatientID = '" + VerifyOrderLab.PatientID + "'";


                SqlCommand cmd = new SqlCommand(str, cn);
                cmd.CommandTimeout = 0;
                //cmd.Parameters.AddWithValue("@patientID", VerifyOrderLab.PatientID);
                //cmd.Parameters.AddWithValue("@VerifiedBy", ViewBag.EmpName);
                //cmd.Parameters.AddWithValue("@VerifiedDt", DateTime.Now);
                //cmd.Parameters.AddWithValue("@Stat", VerifyOrderLab.Stat);
                //cmd.Parameters.AddWithValue("@For_dispute_remarks", VerifyOrderLab.For_dispute_remarks);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();


                return Json("Successful", JsonRequestBehavior.AllowGet);

            }

        }

        // GET: JOinfo/Autocomplete
        [HttpGet]
        public ActionResult Autocomplete(string term)
        {
            // Create a list to store the filtered MedTech objects
            List<MedTech> filteredMedTechs = new List<MedTech>();

            // SQL query to select MedRepID and MedRepName from MedRep_Master table
            string query = "SELECT MedRepName,MedRepID FROM MedRep_Master WHERE MedRepName LIKE @Term";

            using (SqlConnection connection = new SqlConnection(ORDER))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Term", "%" + term + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MedTech medTech = new MedTech
                    {
                        MedRepName = reader["MedRepName"].ToString(),
                        MedRepID = reader["MedRepID"].ToString(),

                    };
                    filteredMedTechs.Add(medTech);
                }
            }

            ViewBag.MedRepID = filteredMedTechs.FirstOrDefault()?.MedRepID;

            // Return the list of filtered MedTech objects as JSON
            return Json(filteredMedTechs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMedRepID(string medRepName)
        {
            string medRepID = ""; // Initialize medRepID variable

            // SQL query to select MedRepID from MedRep_Master table based on MedRepName
            string query = "SELECT MedRepID FROM MedRep_Master WHERE MedRepName = @MedRepName";

            using (SqlConnection connection = new SqlConnection(ORDER))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MedRepName", medRepName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Check if a record with the given MedRepName exists
                if (reader.Read())
                {
                    medRepID = reader["MedRepID"].ToString();
                }
            }

            // Return MedRepID as JSON
            return Json(medRepID, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCount()
        {
            int count = 0;
            try
            {
                string query = "SELECT COUNT(*) FROM Order_Lab WHERE Stat = '2'";
                // Add your database connection string
                using (SqlConnection connection = new SqlConnection(ORDER))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0); // Get the count from the first column
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine(ex.Message);
            }

            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }

}
