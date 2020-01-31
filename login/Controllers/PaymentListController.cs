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

            List<ClientPayment> clist = new List<ClientPayment>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Vipin\Documents\project.mdf;Integrated Security=True");
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
                o.Pid = Convert.ToInt32(dr["Pid"]);
                o.Ammount = Convert.ToDouble(dr["Amount"]);
                
                clist.Add(o);
            }


            return View(clist);

            
        }

        public ActionResult OwnerPaymentList()
        {

            List<OwnerPayment> clist = new List<OwnerPayment>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Vipin\Documents\project.mdf;Integrated Security=True");
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
                o.Pid = Convert.ToInt32(dr["Pid"]);
                o.Amount = Convert.ToDouble(dr["Amount"]);

                clist.Add(o);
            }


            return View(clist);


        }

    }
}