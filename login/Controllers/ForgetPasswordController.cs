using login.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class ForgetPasswordController : Controller
    {
        // GET: ForgetPassword


            public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult InsertUserName(string username)
        {

          //  TempData["otp"] = 1100;
            Session["id"] = username;
            try
            {
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select Type from Users where UserName=@username";


                cmd2.Parameters.AddWithValue("username", username);

                var type = cmd2.ExecuteScalar().ToString();
                if (type == null)
                {
                    Response.Write("< script >alert(Otp not match)  </script>");
                    return RedirectToAction("Login", "LoginId");
                }
                con2.Close();

                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                if (type.Equals("Client"))
                {
                    cmd.CommandText = "select Phone from Client where Email=@username";
                }
                else
                {
                    cmd.CommandText = "select Phone from Owner where Email=@username";
                }

                cmd.Parameters.AddWithValue("@username", username);

                var number = cmd.ExecuteScalar().ToString();
                con.Close();


                string apikey = ConfigurationManager.AppSettings["apiKey"].ToString();

                var status = "";
                Random ran = new Random();
                int otp = ran.Next(1000, 9999);

                TempData["otp"] = otp;

                string msg = " OTP :" + otp;
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

                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }

        public ActionResult Verify(int otp = 1100)
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            {

                if (otp == Convert.ToInt64(TempData["otp"]))
                {
                    return RedirectToAction("UpdatePassword","ForgetPassword");
                }
                else
                {
                    Response.Write("< script >alert(Otp not match)  </script>");
                }
                return RedirectToAction("Login", "LoginId");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "LoginId");
            }

        }

         public ActionResult UpdatePassword()
        {
            return View();
        }
         public ActionResult EditPassWord(string pass)
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
                { 
            EncryptDecrypt e = new EncryptDecrypt();
            string id = Session["id"].ToString();
            SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con2.Open();

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con2;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "update Users set Password=@Pass where UserName=@id";

            cmd2.Parameters.AddWithValue("id", id);
            cmd2.Parameters.AddWithValue("pass", e.Base64Encode(pass));

            cmd2.ExecuteNonQuery();
            con2.Close();

            return RedirectToAction("Login", "LoginId");
            }
                
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }
    }
}