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
    public class PaymentListController : Controller
    {
        // GET: PaymentList
        public ActionResult ClientPaymentList()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            {
                List<ClientPayment> clist = new List<ClientPayment>();
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from ClientPayment";

                SqlDataReader dr = cmd.ExecuteReader();
                //Models.Client o = new Models.Client();
                

                while (dr.Read())
                {
                    ClientPayment o = new ClientPayment();
                    o.Cid = Convert.ToInt32(dr["Cid"]);
                    o.Date = Convert.ToString(dr["Date"]);
                    o.TransactionId = (dr["TransactionId"]).ToString();
                    o.Ammount = Convert.ToDouble(dr["Amount"]);

                    clist.Add(o);
                }


                return View(clist);

            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }

        public ActionResult OwnerPaymentList()
        {


            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "LoginId");
            }
            try
            { 
            List<OwnerPayment> clist = new List<OwnerPayment>();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from OwnerPayment";

            SqlDataReader dr = cmd.ExecuteReader();
            //Models.Client o = new Models.Client();

            while (dr.Read())
            {
                OwnerPayment o = new OwnerPayment();
                o.Oid = Convert.ToInt32(dr["Oid"]);
                o.Date = Convert.ToString(dr["Date"]);
                    o.TransactionId = (dr["TransactionId"]).ToString();
                    o.Amount = Convert.ToDouble(dr["Amount"]);

                clist.Add(o);
            }


            return View(clist);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }

        }

    }
}