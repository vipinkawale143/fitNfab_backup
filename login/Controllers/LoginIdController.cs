using login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace login.Controllers
{
    public class LoginIdController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }


        // POST: LoginId/Create
        [HttpPost]
        public ActionResult Login(string username,string pass)
        {
     
            try
            {
                EncryptDecrypt b = new EncryptDecrypt();

                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
               

                cmd.CommandText = "select * from Users where UserName=@UserName and Password=@Password and Flag=1";


                cmd.Parameters.AddWithValue("Password",b.Base64Encode(pass));
                var x = b.Base64Encode(pass);
                cmd.Parameters.AddWithValue("UserName", username);

                SqlDataReader rd = cmd.ExecuteReader();

                

                if (rd.Read())
                {
                    var type = Convert.ToString(rd["Type"]);

                    Session["id"] =Convert.ToString(rd["UserName"]);
                   

                    ViewBag.successMessage = "Login succesfull";

                    if (type.Equals("Client"))
                    {


                        return RedirectToAction("ClientView", "Default");
                    }
                    else if (type.Equals("Owner"))
                    {

                        return RedirectToAction("OwnerView", "BookingList");

                    }
                    else if (type.Equals("Admin"))
                    {

                        return RedirectToAction("AdminView", "Default");

                    }


                }




                con.Close();
                ViewBag.successMessage = "Please register first";
                    return RedirectToAction("Login");
               
                
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return View();
            }
        }

       



    }
}
