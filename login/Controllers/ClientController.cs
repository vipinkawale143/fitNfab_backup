using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using login.Models;

namespace login.Controllers

{
    public class ClientController : Controller
    {
        static string s = "";


        // GET: Client
        public ActionResult ClientList()
        {
            try
            { 
            List<Client> clist = new List<Client>();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Client where Flag=1";

            SqlDataReader dr = cmd.ExecuteReader();
            //Models.Client o = new Models.Client();

            while (dr.Read())
            {
                Client o = new Client();
                o.Cid = Convert.ToInt32(dr["Cid"]);
                o.Name = Convert.ToString(dr["Name"]);
                o.Email = Convert.ToString(dr["Email"]);
                o.Phone = Convert.ToInt64(dr["Phone"]);
                o.Membership = Convert.ToBoolean(dr["Membership"]);
                o.Age = Convert.ToInt32(dr["Age"]);
                o.Gender = Convert.ToString(dr["Gender"]);
                clist.Add(o);
            }


            return View(clist);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }

        //  GET: Client/Details/5
        public ActionResult ClientDetails(int id = 0)
        {
            try
            { 
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            
            
               
                string username = Convert.ToString(Session["id"]);
                cmd.CommandText = "select * from  Client where Email=@username and Flag=1";
                cmd.Parameters.AddWithValue("@username", username);
            
            

            SqlDataReader dr = cmd.ExecuteReader();
            Models.Client o = new Models.Client();
            if (dr.Read())
            { 
                o.Cid = Convert.ToInt32(dr["Cid"]);
                o.Email = Convert.ToString(dr["Email"]);
                o.Name = Convert.ToString(dr["Name"]);
                 o.Phone = Convert.ToInt64(dr["Phone"]);
                o.Age = Convert.ToInt32(dr["Age"]);
                o.Gender = Convert.ToString(dr["Gender"]);
                o.Membership = Convert.ToBoolean(dr["Membership"]);


            }

            con.Close();

            return View(o);

            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }


        }

        [HttpPost]
        public JsonResult ValidateEmail(Client o)
        {
            s = o.Email;
            try
            {
                EncryptDecrypt e = new EncryptDecrypt();
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                string pass = String.Empty;
                if (o.Email != null)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "select UserName from Users where UserName=@UserName";
                    cmd2.Parameters.AddWithValue("@UserName", o.Email);
                    SqlDataReader dr = cmd2.ExecuteReader();

                    if (dr.Read())
                    {
                        pass = Convert.ToString(dr["UserName"]);
                    }
                }
                return Json(pass);
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('Email already registered')</script>");
                return Json("");
            }
        }
        // GET: Client/Create
        public ActionResult ClientRegistration()
               {
                     return View();
             }

        // POST: Client/Create
        [HttpPost]
        public ActionResult ClientRegistration(Client o)
        {
            
            try
            {
                EncryptDecrypt b = new EncryptDecrypt();
                if ((o.Name.Equals(""))||(o.Email.Equals(""))||o.Password.Equals("")||(o.Phone<1000000000)&&(o.Phone>9999999999)||(o.Age<12)&&(o.Age>80))
                {
                    ViewBag.message = "Fill details correctly";
                    return RedirectToAction("ClientRegistraion", "Client");

                }
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Users values(@UserName,@Password,@Type,@Flag)";
                cmd.Parameters.AddWithValue("UserName", o.Email);
                cmd.Parameters.AddWithValue("Password",b.Base64Encode(o.Password));
                cmd.Parameters.AddWithValue("Flag",1);
                cmd.Parameters.AddWithValue("Type", "Client");
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Client values(@Email,@Name,@Phone,@Gender,@Age,@Membership,@Flag)";
                cmd.Parameters.AddWithValue("Email", o.Email);
                cmd.Parameters.AddWithValue("Name", o.Name);
                cmd.Parameters.AddWithValue("Phone", o.Phone);
                cmd.Parameters.AddWithValue("Gender", o.Gender);
                cmd.Parameters.AddWithValue("Age", o.Age);
                cmd.Parameters.AddWithValue("Membership", 0);
                cmd.Parameters.AddWithValue("Flag",1);
                
                cmd.ExecuteNonQuery();

                con.Close();
                ViewBag.Message = "registered succsessfuly";

                return RedirectToAction("Login", "LoginId");
                
            }
            catch(Exception e)
            {
               
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult ClientEdit(int id=0)
        {
            try
            { 
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from  Client where Email=@UserName";
            string username = Convert.ToString(Session["id"]);
            cmd.Parameters.AddWithValue("UserName", username);
            SqlDataReader dr = cmd.ExecuteReader();

          
            Models.Client o = new Models.Client();
            if (dr.Read())
            {
               // o.Cid = Convert.ToInt32(dr["Cid"]);
                o.Email = Convert.ToString(dr["Email"]);
                o.Name = Convert.ToString(dr["Name"]);
                //o.Password = Convert.ToString(dr["Password"]);
                o.Phone = Convert.ToInt64(dr["Phone"]);
                o.Age = Convert.ToInt32(dr["Age"]);
                o.Gender = Convert.ToString(dr["Gender"]);

            }

            con.Close();
            return View(o);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }

        //// POST: Client/Edit/5
        [HttpPost]
        public ActionResult ClientEdit(Models.Client o)
        {
            try
            {
                

                string username = Convert.ToString(Session["id"]);
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "update Client set Name=@name,Phone=@phone,Gender=@gen,Age=@age,Membership=@mem where Email=@email";
               
                cmd2.Parameters.AddWithValue("@name", o.Name);
                cmd2.Parameters.AddWithValue("@phone", o.Phone);
                cmd2.Parameters.AddWithValue("@gen", o.Gender);
                cmd2.Parameters.AddWithValue("@age", o.Age);
                cmd2.Parameters.AddWithValue("@mem", o.Membership);
               
                cmd2.Parameters.AddWithValue("@email", username);
                cmd2.ExecuteNonQuery();
                con2.Close();

                    return RedirectToAction("ClientDetails", "Client");
                }
            catch (Exception e)
            {
                return View();
            }
        }

        //// GET: Client/Delete/5
        public ActionResult ClientDelete(int id = 0)
        {
            try
            {
                if (Session["id"] == null)
                {
                    return RedirectToAction("Login", "LoginId");
                }

                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from  Client where Email=@Email";
                string username = Convert.ToString(Session["id"]);
                cmd.Parameters.AddWithValue("Email", username);
                SqlDataReader dr = cmd.ExecuteReader();


                Models.Client o = new Models.Client();
                if (dr.Read())
                {

                    o.Email = Convert.ToString(dr["Email"]);
                    o.Name = Convert.ToString(dr["Name"]);

                    o.Phone = Convert.ToInt64(dr["Phone"]);
                    o.Age = Convert.ToInt32(dr["Age"]);
                    o.Gender = Convert.ToString(dr["Gender"]);

                }

                con.Close();
                return View(o);
            
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }

}
        public ActionResult DummyView()
        {
            return View();
        }
        //// POST: Client/Delete/5
        [HttpPost]
        public ActionResult ClientDeletePermanant(string Email )
        {
            try
            {

                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " update Client set Flag=0 where Email=@Email";



                cmd.Parameters.AddWithValue("@Email",Email);
                int i = cmd.ExecuteNonQuery();
                con.Close();



                SqlConnection con1 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con1.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandType = CommandType.Text;
            
                cmd1.CommandText = " update Users set Flag=0 where UserName=@UserName";



                cmd1.Parameters.AddWithValue("@UserName",Email );
                int j = cmd1.ExecuteNonQuery();
                con1.Close();
                return RedirectToAction("Login","LoginId");
            }
            catch
            {
                return View();
            }
        }


    }
}
