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
            List<Client> clist = new List<Client>();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Client";

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

        //  GET: Client/Details/5
        public ActionResult ClientDetails(int id = 0)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //details for admin or owner
            if (id != 0)
            {
                cmd.CommandText = "select * from  Client where Cid=@Cid";
                cmd.Parameters.AddWithValue("@Cid", id);
            }
            //detail for own    
           else if (id == 0)
            {
                string username = Convert.ToString(Session["id"]);
                cmd.CommandText = "select * from  Client where Email=@username";
                cmd.Parameters.AddWithValue("@username", username);
            }
            

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
            EncryptDecrypt b = new EncryptDecrypt();
            try
            {
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Users values(@UserName,@Password,@Type,@Flag)";
                cmd.Parameters.AddWithValue("UserName", s);
                cmd.Parameters.AddWithValue("Password",b.Base64Encode(o.Password));
                cmd.Parameters.AddWithValue("Flag",1);
                cmd.Parameters.AddWithValue("Type", "Client");
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Client values(@Email,@Name,@Phone,@Gender,@Age,@Membership,@Flag)";
                cmd.Parameters.AddWithValue("Email", s);
                cmd.Parameters.AddWithValue("Name", o.Name);
                cmd.Parameters.AddWithValue("Phone", o.Phone);
                cmd.Parameters.AddWithValue("Gender", o.Gender);
                cmd.Parameters.AddWithValue("Age", o.Age);
                cmd.Parameters.AddWithValue("Membership", 0);
                cmd.Parameters.AddWithValue("Flag",1);
                
                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('data entered successfully')</script>");

                return RedirectToAction("Login", "LoginId");
                
            }
            catch(Exception e)
            {
               // Response.Write("<script>alert('primary key voilation')</script>");
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult ClientEdit(int id=0)
        {
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

        //// POST: Client/Edit/5
        [HttpPost]
        public ActionResult ClientEdit(Models.Client o)
        {
            try
            {
                //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                //con.Open();
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = con;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "select Flag from  Client where Email=@UserName";
                //string username = Convert.ToString(Session["id"]);
                //cmd.Parameters.AddWithValue("UserName", username);

                //bool flag  = Convert.ToBoolean(cmd.ExecuteScalar());
                //con.Close();

                //SqlConnection con1 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                //con.Open();
                //SqlCommand cmd1 = new SqlCommand();
                //cmd1.Connection = con1;
                //cmd1.CommandType = CommandType.Text;
                //cmd1.CommandText = "update Users set Password =@pass where Flag=@flag";

                //cmd1.Parameters.AddWithValue("@pass", o.Password);
                //cmd1.Parameters.AddWithValue("@flag", flag);
                //cmd1.ExecuteNonQuery();
                //con1.Close();


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
        //public ActionResult ClientDelete(int Cid)
        //{

        //    SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project14;Integrated Security=True");
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select * from  Client where Cid=@Cid";


        //    cmd.Parameters.AddWithValue("@Cid", Cid);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    Models.Client o = new Models.Client();
        //    if (dr.Read())
        //    {
        //        o.Cid = Convert.ToInt32(dr["Cid"]);
        //        o.Name = Convert.ToString(dr["Name"]);
        //       // o.Password = "*****";
        //        o.Membership = Convert.ToString(dr["Membership"]);



        //        o.Phone = Convert.ToInt64(dr["Phone"]);
        //        o.Age = Convert.ToInt32(dr["Age"]);
        //        o.Gender = Convert.ToString(dr["Gender"]);

        //    }

        //    con.Close();
        //    return View(o);



        //}

        //// POST: Client/Delete/5
        //[HttpPost]
        //public ActionResult ClientDelete(int Cid, Client o)
        //{
        //    try
        //    {

        //        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project14;Integrated Security=True");
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "delete from Client where Cid=@Cid";

        //        cmd.Parameters.AddWithValue("@Cid", o.Cid);
        //        int i = cmd.ExecuteNonQuery();
        //        con.Close();

        //        return RedirectToAction("ClientList");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }
}
