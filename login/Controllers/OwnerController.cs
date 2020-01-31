using login.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Web.UI;

namespace login.Controllers
{
    public class OwnerController : Controller
    {

        static string s = "";
        // GET: Owner
        public ActionResult OwnerList(string city=null)
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try { 
            List<Owner> olist = new List<Owner>();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if (city == null)
            {
                cmd.CommandText = "select * from Owner where Flag=1";
            }
            else
            {
                cmd.CommandText = "select * from Owner where Flag=1 and Address Like @city";
                cmd.Parameters.AddWithValue("@city", "%"+city+"%");
               
            }

            SqlDataReader dr = cmd.ExecuteReader();
            

            while (dr.Read())
            {
                Owner o = new Owner();
                o.Oid = Convert.ToInt32(dr["Oid"]);
                o.Name = Convert.ToString(dr["Name"]);
                o.Email = Convert.ToString(dr["Email"]);
                o.Phone = Convert.ToInt64(dr["Phone"]);
                o.Description = Convert.ToString(dr["Description"]);
                o.Address = Convert.ToString(dr["Address"]);
                o.Latitude = Convert.ToDouble(dr["Latitude"]);
                o.Longitude = Convert.ToDouble(dr["Longitude"]);

                olist.Add(o);
                


            }


            return View(olist);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }


        public ActionResult OwnerListForAdmin()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            {
                List<Owner> olist = new List<Owner>();
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                
                    cmd.CommandText = "select * from Owner where Flag=1";
                
               

                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Owner o = new Owner();
                    o.Oid = Convert.ToInt32(dr["Oid"]);
                    o.Name = Convert.ToString(dr["Name"]);
                    o.Email = Convert.ToString(dr["Email"]);
                    o.Phone = Convert.ToInt64(dr["Phone"]);
                    o.Description = Convert.ToString(dr["Description"]);
                    o.Address = Convert.ToString(dr["Address"]);
                    o.Latitude = Convert.ToDouble(dr["Latitude"]);
                    o.Longitude = Convert.ToDouble(dr["Longitude"]);

                    olist.Add(o);



                }


                return View(olist);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }
        //// GET: Owner/Details/5
        public ActionResult OwnerDetails(int id=0)
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                //details for admin or owner
                if (id != 0)
                {
                    cmd.CommandText = "select * from  Owner where Oid=@Oid";
                    cmd.Parameters.AddWithValue("@Oid", id);
                }
                //detail for own    
                else if (id == 0)
                {
                    string username = Convert.ToString(Session["id"]);
                    cmd.CommandText = "select * from  Owner where Email=@username";
                    cmd.Parameters.AddWithValue("@username", username);
                }


                SqlDataReader dr = cmd.ExecuteReader();
                Owner o = new Owner();
                if (dr.Read())
                {
                    o.Oid = Convert.ToInt32(dr["Oid"]);
                    o.Email = Convert.ToString(dr["Email"]);
                    o.Name = Convert.ToString(dr["Name"]);
                    o.Phone = Convert.ToInt64(dr["Phone"]);
                    o.Address = Convert.ToString(dr["Address"]);
                    o.Description = Convert.ToString(dr["Description"]);
                    o.Latitude = Convert.ToDouble(dr["Latitude"]);
                    o.Longitude = Convert.ToDouble(dr["Longitude"]);
                    TempData["Lat"] = o.Latitude;
                    TempData["Lon"] = o.Longitude;
                    TempData["Name"] = o.Name;


                }
                con.Close();

                return View(o);
 
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
    }
}


        public ActionResult BookSession(int id)
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }

            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Client where Email=@UserName";
                string username = Convert.ToString(Session["id"]);

                cmd.Parameters.AddWithValue("UserName", username);
                SqlDataReader dr = cmd.ExecuteReader();
                
                dr.Read();
                string number = Convert.ToString(dr["Phone"]);
                int Cid = Convert.ToInt32(dr["Cid"]);

                if (Convert.ToBoolean(dr["Membership"]))
                {
                    SqlConnection con3 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                    con3.Open();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con3;
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "select * from Booking where Cid=@Cid and Date=cast(getdate() as Date)";
                    
                    cmd3.Parameters.AddWithValue("Cid", Cid);
                    SqlDataReader dr4 = cmd3.ExecuteReader();
                    if(dr4.Read())
                    {


                        //ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}');","alredy");
                        ViewBag.BookedSession = "already Booked";
                        Response.Write("<script>alert('booked already')</script>");
                        return RedirectToAction("OwnerList", "Owner");
                    }

                    Random rand = new Random();
                    int bid = rand.Next(1000, 9999);
                    int Tid = rand.Next(100000,999999);


                    SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                    con2.Open();

                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "insert into OwnerPayment values(@Oid,50,GETDATE(),@Tid)";
                    cmd2.Parameters.AddWithValue("Tid", Tid);
                    cmd2.Parameters.AddWithValue("Oid", id);

                    cmd2.ExecuteNonQuery();


                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "update OwnerAccountDetails set CurrentAmount=CurrentAmount+50 where Oid = @Oid ";
                    cmd2.Parameters.AddWithValue("Oid", id);
                    cmd2.ExecuteNonQuery();


                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "update AdminBankAccount set CurrentBalance=CurrentBalance-50  ";
                    cmd2.ExecuteNonQuery();


                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "insert into Booking values(@BookingId,@Cid,@Oid,GETDATE(),'Unchecked')";

                    cmd2.Parameters.AddWithValue("BookingId", bid);
                    cmd2.Parameters.AddWithValue("Cid", Cid);
                    cmd2.Parameters.AddWithValue("Oid", id);

                    cmd2.ExecuteNonQuery();
                    con2.Close();

                    try
                    {

                        string apikey = ConfigurationManager.AppSettings["apiKey"].ToString();

                        var status = "";

                        string msg = "Booking Id :" + bid;
                        string enCode = HttpUtility.UrlEncode(msg);

                        using (var webClient = new WebClient())
                        {

                            byte[] res = webClient.UploadValues("https://api.textlocal.in/send/", new System.Collections.Specialized.NameValueCollection() {

                    {"apiKey",apikey},
                    {"numbers" ,number},
                    {"message",enCode },
                    { "sender","TXTLCL"} });

                            string result = System.Text.Encoding.UTF8.GetString(res);

                            var jsonObj = JObject.Parse(result);
                            status = jsonObj["status"].ToString();

                        }
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("ClientView", "Default");
                    }

                    return RedirectToAction("ClientView", "Default");
                }




                con.Close();
                Response.Write("<script>alert('get membership first')</script>");
               
                return RedirectToAction("BookPlan", "Default");

            }
            catch(Exception e)
            {
                return RedirectToAction("Login", "LoginId");
            }

        }
       // [HttpPost]
        //public JsonResult ValidateEmail(Client o)
        //{
        //    s = o.Email;
        //    try
        //    {
        //        EncryptDecrypt e = new EncryptDecrypt();
        //        // TODO: Add insert logic here
        //        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
        //        con.Open();
        //        string pass = String.Empty;
        //        if (o.Email != null)
        //        {
        //            SqlCommand cmd2 = new SqlCommand();
        //            cmd2.Connection = con;
        //            cmd2.CommandType = CommandType.Text;
        //            cmd2.CommandText = "select UserName from Users where UserName=@UserName";
        //            cmd2.Parameters.AddWithValue("@UserName", o.Email);
        //            SqlDataReader dr = cmd2.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                pass = Convert.ToString(dr["UserName"]);
        //            }
        //        }
        //        return Json(pass);
        //    }
        //    catch (Exception e)
        //    {
        //        Response.Write("<script>alert('Email already registered')</script>");
        //        return Json("");
        //    }
        //}
        // GET: Owner/Create
        public ActionResult OwnerRegistration()
        {

            return View();
        }

        // POST: Owner/Create
        [HttpPost]
        public ActionResult OwnerRegistration(Owner o)
        {
            SqlTransaction trans = null;
            EncryptDecrypt e = new EncryptDecrypt();
            try
            {

               
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                trans = con.BeginTransaction("abc");
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Users values(@UserName,@Password,@Type,@Flag)";
                cmd.Parameters.AddWithValue("UserName", o.Email);
                cmd.Parameters.AddWithValue("Password", e.Base64Encode(o.Password));
                cmd.Parameters.AddWithValue("Flag", 1);
                cmd.Parameters.AddWithValue("Type", "Owner");
                trans.Save("saveusers");
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Owner values(@Email,@Name,@Phone,@Description,@Address,@Latitude,@Longitude,@Flag)";
                cmd.Parameters.AddWithValue("Email", o.Email);
                cmd.Parameters.AddWithValue("Name", o.Name);
                cmd.Parameters.AddWithValue("Phone", o.Phone);
                cmd.Parameters.AddWithValue("Description", o.Description);
                cmd.Parameters.AddWithValue("Address", o.Address);
                cmd.Parameters.AddWithValue("Latitude", o.Latitude);
                cmd.Parameters.AddWithValue("Longitude", o.Longitude);
                cmd.Parameters.AddWithValue("Flag", 0);
               
                cmd.ExecuteNonQuery();
                trans.Commit();
                con.Close();
                ViewBag.Message = "registerd successfuly";

                return RedirectToAction("Login", "LoginId");
             
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "error";
                trans.Rollback();
                return View();
               
            }
        }

        //// GET: Owner/Edit/5
        public ActionResult OwnerEdit(int id=0)
        {


            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }

            try
            { 
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from  Owner where Email=@UserName";
            string username = Convert.ToString(Session["id"]);
            cmd.Parameters.AddWithValue("UserName", username);
            SqlDataReader dr = cmd.ExecuteReader();


            Models.Owner o = new Models.Owner();
            if (dr.Read())
            {
                 o.Email = Convert.ToString(dr["Email"]);
                 o.Name = Convert.ToString(dr["Name"]);  
                 o.Description = Convert.ToString(dr["Description"]);
                 o.Phone = Convert.ToInt64(dr["Phone"]);
                 o.Address = Convert.ToString(dr["Address"]);
                 o.Latitude = Convert.ToDouble(dr["Latitude"]);
                 o.Longitude = Convert.ToDouble(dr["Longitude"]);

            }

            con.Close();
            return View(o);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "LoginId");
            }
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult OwnertEdit(int id, Models.Owner o)
        {
            try
            {
               
                string username = Convert.ToString(Session["id"]);
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "update Owner set Name=@Name,Phone=@Phone,Description=@Description,Address=@Address where Email=@Email";

                cmd2.Parameters.AddWithValue("Name", o.Name);
                cmd2.Parameters.AddWithValue("Phone", o.Phone);
                cmd2.Parameters.AddWithValue("Description", o.Description);
                cmd2.Parameters.AddWithValue("Address", o.Address);
                cmd2.Parameters.AddWithValue("Email", username);
                cmd2.ExecuteNonQuery();
                con2.Close();

                return RedirectToAction("OwnerDetails", "Owner");
            }
            catch
            {
                return View();
            }
        }

      

        public ActionResult Location()
        {
            try
            {
                ViewBag.Latitude = TempData["Lat"];
                ViewBag.Longitude = TempData["Lon"];
                ViewBag.Name = TempData["Name"];
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }

        }
        public ActionResult AllLocations()
        {
            try
            { 
            List<Owner> ol = new List<Owner>();
            Owner o = new Owner();
            o.Latitude = 11.11;
            o.Longitude = 12.13;

            Owner o1 = new Owner();
            o1.Latitude = 11.11;
            o1.Longitude = 12.13;
            ol.Add(o);
            ol.Add(o);
            ViewBag.ol = ol;
            return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }


        

    }
}
