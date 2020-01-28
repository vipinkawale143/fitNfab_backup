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
    public class DefaultController : Controller
    {
        public ActionResult ClientView()
        {
            
            
            return View();
        }
        ////[HttpPost]
        public ActionResult BookPlan()
        {
            //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project14;Integrated Security=True");
            //con.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;

            return View();
        }

        //public ActionResult Location(string Latitude, string Longitude, string Name)
        //{
        //    @ViewBag.Latitude = Latitude;
        //    @ViewBag.Longitude = Longitude;
        //    @ViewBag.Name = Name;
        //    return View();
        //}

        // GET: Default
        public ActionResult BookClientList()
        {

            SqlConnection con2 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con2.Open();


            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con2;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select Oid from Owner where Email=@UserName";
            cmd2.Parameters.AddWithValue("@UserName", Convert.ToString(Session["id"]));
            object Oid = cmd2.ExecuteScalar();



            List<Booking> clist = new List<Booking>();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Booking where Date=cast(getdate() as date) and Oid=@Oid";
            cmd.Parameters.AddWithValue("@Oid", Convert.ToInt32(Oid));

            SqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {

                SqlConnection con3 = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
                con3.Open();


                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con3;
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "select Name from Client where Cid=@Cid";
                cmd3.Parameters.AddWithValue("@Cid", Convert.ToInt32(dr["Cid"]));

                string name = cmd3.ExecuteScalar().ToString();

                
                Booking o = new Booking();
                o.Cid = Convert.ToInt32(dr["Cid"]);
               

                o.Oid = Convert.ToInt32(dr["Oid"]);

                o.BookingId = Convert.ToInt32(dr["BookingId"]);
                o.Status = Convert.ToString(dr["Status"]);
                o.Date = name;
                clist.Add(o);
            }


            return View(clist);

        }

            public ActionResult AdminBankDetails()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from AdminBankAccount ";

            SqlDataReader dr = cmd.ExecuteReader();
            //Models.Client o = new Models.Client();

            AdminBankDetail o = new AdminBankDetail();

            if (dr.Read())
            {
                
                o.BankHolderName = Convert.ToString(dr["BankHolderName"]);
                o.AccountNumber = Convert.ToInt64(dr["AccountNumber"]);
                o.BankName = Convert.ToString(dr["BankName"]);
                o.IFSC = Convert.ToString(dr["IFSC_Code"]);
                o.CurrentAmount = Convert.ToDouble(dr["CurrentBalance"]);
               
            }


            return View(o);



            
        }


        public ActionResult CheckedIn(int bid,int oid)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=project;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update  Booking set Status='Checked' where BookingId=@bid and Oid=@oid";

            cmd.Parameters.AddWithValue("@bid", bid);
            cmd.Parameters.AddWithValue("@Oid", oid);
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("BookClientList", "Default");
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AdminView()
        {
            return View();
        }

    }
}
