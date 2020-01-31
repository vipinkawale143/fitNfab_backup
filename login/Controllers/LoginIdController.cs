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


            try
            {
                if (Session["id"] != null)
                {
                    SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                    con2.Open();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.Text;


                    cmd2.CommandText = "select * from Users where UserName=@UserName  Flag=1";


                    cmd2.Parameters.AddWithValue("UserName", Session["id"].ToString());



                    SqlDataReader rd2 = cmd2.ExecuteReader();



                    if (rd2.Read())
                    {
                        var type = Convert.ToString(rd2["Type"]);



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



                }
                return View();
            }
            catch(Exception e)
            {
                return View();
            }
        }


        // POST: LoginId/Create
        [HttpPost]
        public ActionResult Login(string username,string pass)
        {

            try { 
                EncryptDecrypt b = new EncryptDecrypt();

                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
               

                cmd.CommandText = "select * from Users where UserName=@UserName and Password=@Password and Flag=1";


                cmd.Parameters.AddWithValue("Password",b.Base64Encode(pass));
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
                ViewBag.Message = "Invalid Credentials !";
                
                return View();
               
                
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return View();
            }
        }

       



    }
}
