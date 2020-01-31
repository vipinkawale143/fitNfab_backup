using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace login.Controllers
{
    public class ClientPaymentController : Controller
    {
        // GET: ClientPayment
        public ActionResult MonthlyPayment()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }

            try { 
            string email = Session["id"].ToString();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select cid from Client where Email = @email";
            cmd.Parameters.AddWithValue("email", email);

            int cid =Convert.ToInt32(cmd.ExecuteScalar());
                Random ran = new Random();
                int no = ran.Next(100000,999999);
                double amount = 2000;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into ClientPayment values(@Cid,@amount,getdate(),@Tid)";
            cmd.Parameters.AddWithValue("Cid", cid);
            cmd.Parameters.AddWithValue("Tid", no);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();


            cmd.Parameters.Clear();
            cmd.CommandText = "update  Client set Membership=1 where Cid = @Cid";
            cmd.Parameters.AddWithValue("Cid", cid);

            cmd.ExecuteNonQuery();


                cmd.Parameters.Clear();
                cmd.CommandText = "update  AdminBankAccount set CurrentBalance=CurrentBalance + @amount ";
                
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();

                con.Close();
            Response.Write("<script>alert('data entered successfully')</script>");

            return RedirectToAction("ClientView", "Default");

        }
            catch (Exception e)
            {
                return RedirectToAction("Login", "LoginId");
    }


}

        public ActionResult WeeklyPayment()
        {


            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }

            try { 
            string email = Session["id"].ToString();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select cid from Client where Email = @email";
            cmd.Parameters.AddWithValue("email", email);

            int cid = Convert.ToInt32(cmd.ExecuteScalar());
                double amount = 700;

                Random ran = new Random();
                int no = ran.Next(100000, 999999);


                cmd.Parameters.Clear();
            cmd.CommandText = "insert into ClientPayment values(@Cid,@amount,getdate(),@Tid)";
            cmd.Parameters.AddWithValue("Cid", cid);
            cmd.Parameters.AddWithValue("Tid", no);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();


            cmd.Parameters.Clear();
            cmd.CommandText = "update  Client set Membership=1 where Cid = @Cid";
            cmd.Parameters.AddWithValue("Cid", cid);

            cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = "update  AdminBankAccount set CurrentBalance=CurrentBalance + @amount ";

                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();

                con.Close();
            Response.Write("<script>alert('data entered successfully')</script>");

            return RedirectToAction("ClientView", "Default");

        }
            catch (Exception e)
            {
                return RedirectToAction("Login", "LoginId");
    }

}

        public ActionResult QuaterlyPayment()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            {
                string email = Session["id"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select cid from Client where Email = @email";
                cmd.Parameters.AddWithValue("email", email);

                int cid = Convert.ToInt32(cmd.ExecuteScalar());
                double amount = 5000;

                Random ran = new Random();
                int no = ran.Next(100000, 999999);


                cmd.Parameters.Clear();
                cmd.CommandText = "insert into ClientPayment values(@Cid,@amount,getdate(),@Tid)";
                cmd.Parameters.AddWithValue("Cid", cid);
                cmd.Parameters.AddWithValue("Tid", no);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();


                cmd.Parameters.Clear();
                cmd.CommandText = "update  Client set Membership=1 where Cid = @Cid";
                cmd.Parameters.AddWithValue("Cid", cid);

                cmd.ExecuteNonQuery();


                cmd.Parameters.Clear();
                cmd.CommandText = "update  AdminBankAccount set CurrentBalance=CurrentBalance + @amount ";

                cmd.Parameters.AddWithValue("amount", amount);
                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('data entered successfully')</script>");

                return RedirectToAction("ClientView", "Default");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "LoginId");
            }

        }

    }
}