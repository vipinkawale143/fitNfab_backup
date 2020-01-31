using login.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class OwnerAccountDetailsController : Controller
    {
        // GET: OwnerAccountDetails
        public ActionResult AddAccount()
        {
            try
            {
                string id = Session["id"].ToString();
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from Owner where Email=@email";
                cmd2.Parameters.AddWithValue("email", id);

                int Oid = Convert.ToInt32(cmd2.ExecuteScalar());

                cmd2.Parameters.Clear();
                cmd2.CommandText = "select * from OwnerAccountDetails where Oid=@Oid";
                cmd2.Parameters.AddWithValue("Oid", Oid);
                SqlDataReader dr = cmd2.ExecuteReader();
                if(dr.Read())
                {
                    ViewBag.Message = "Acount Allredy added";
                    return RedirectToAction("OwnerDetails", "Owner");
                }
                return View();
            }
            catch
            {
                  return RedirectToAction("Login", "LoginId"); 
            }
        }


        [HttpPost]
        public ActionResult AddAccount(OwnerAccountDetails o)
        {
            

            try
            {
                string id = Session["id"].ToString();
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select Oid from Owner where Email=@email";
                cmd2.Parameters.AddWithValue("email",id);
                
                int Oid = Convert.ToInt32(cmd2.ExecuteScalar());


                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into OwnerAccountDetails values(@oid,@name,@accountnumber,@bankname,@ifsc,0) ";
                cmd.Parameters.AddWithValue("oid", Oid);
                cmd.Parameters.AddWithValue("name", o.BankHolderName);
                cmd.Parameters.AddWithValue("accountnumber", o.AccountNumber);
                cmd.Parameters.AddWithValue("bankname", o.BankName);
                cmd.Parameters.AddWithValue("ifsc", o.IFSC);
               
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update owner set Flag = 1 where Oid=@Oid ";
                cmd.Parameters.AddWithValue("Oid", Oid);

                cmd.ExecuteNonQuery();


                con.Close();
                ViewBag.Message = "registered succsessfuly";

                return RedirectToAction("OwnerDetails", "Owner");

            }
            catch (Exception e)
            {
                return View();
            }

        }

        public ActionResult AccountDetails()
        {

            try
            {
                string id = Session["id"].ToString();
                SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from Owner where Email=@email";
                cmd2.Parameters.AddWithValue("email", id);

                int Oid = Convert.ToInt32(cmd2.ExecuteScalar());

                cmd2.Parameters.Clear();
                cmd2.CommandText = "select * from OwnerAccountDetails where Oid=@Oid";
                cmd2.Parameters.AddWithValue("Oid", Oid);
                SqlDataReader dr = cmd2.ExecuteReader();
                OwnerAccountDetails o = new OwnerAccountDetails();
                if (dr.Read())
                {

                    
                    o.BankHolderName = dr["BankHolderName"].ToString();
                    o.AccountNumber = Convert.ToInt64( dr["AccountNumber"]);
                    o.BankName = dr["BankName"].ToString();
                    o.IFSC = dr["IFSC_Code"].ToString();
                    o.CurrentAmount =Convert.ToDouble( dr["CurrentAmount"]);


                }
                return View(o);
            }
            catch
            {
                return RedirectToAction("Login", "LoginId");
            }


           
        }
    }
}