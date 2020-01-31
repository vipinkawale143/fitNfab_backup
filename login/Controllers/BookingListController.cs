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
    public class BookingListController : Controller
    {
        // GET: BookingList
        public ActionResult OwnerView()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("LoginId","Login");
            }
            return RedirectToAction("BookClientList", "Default");
        }
        public ActionResult BookingHistory()
        {
            try
            {
                List<Booking> clist = new List<Booking>();
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select client.name as CName, owner.Name as OName ,BookingId,Date from client join Booking on Client.Cid = Booking.Cid join owner on Booking.Oid =Owner.Oid";

                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Booking o = new Booking();
                    o.CName = Convert.ToString(dr["CName"]);
                    o.OName = Convert.ToString(dr["OName"]);
                    o.BookingId = Convert.ToInt32(dr["BookingId"]);
                    o.Date = Convert.ToString(dr["Date"]);
                    clist.Add(o);
                }


                return View(clist);
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginId", "Login");
            }
        }


        // GET: BookingList

    }
}