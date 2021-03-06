﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookstore.Models;
using System.Data.SqlClient;
using MyBookstore.App_Code;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace MyBookstore.Controllers
{
    public class AuthorsController : Controller
    {
        
        public int AuthorsModel { get; private set; }

        [HttpGet]
        // GET: Authors
        public ActionResult Index()
        {
            List<AuthorsModels> list = new List<AuthorsModels>();
            using (SqlConnection con = new SqlConnection(Katulong.GetCon()))
            {
                string query = @"SELECT authorID, authorLN, authorFN , authorPhone, authorAddress, authorCity, authorState, authorZip FROM authors";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            foreach(DataRow row in dt.Rows)
                            {
                                var author = new AuthorsModels();
                                author.ID = Convert.ToInt32(row["authorID"].ToString());
                                author.LastName = row["authorLN"].ToString();
                                author.FirstName = row["authorFN"].ToString();
                                author.Phone = row["authorPhone"].ToString();
                                author.Address = row["authorAddress"].ToString();
                                author.City = row["authorCity"].ToString();
                                author.State = row["authorState"].ToString();
                                author.Zip = row["authorZip"].ToString();
                                list.Add(author);


                            }
                        }
                    }
                }

            }

                return View(list);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Authors/Create
        public ActionResult Create()
        {






            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        public ActionResult Create(AuthorsModels authors)
        {
            /* try
             {
                 // TODO: Add insert logic here

                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }

     */

            using (SqlConnection con = new SqlConnection(Katulong.GetCon()))


            {
                con.Open();
                string query = @"INSERT INTO authors VALUES (@authorLN, @authorFN, @authorPhone, @authorAddress, @authorCity, @authorState, @authorZip)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@authorLN", authors.LastName);
                    cmd.Parameters.AddWithValue("@authorFN", authors.FirstName);
                    cmd.Parameters.AddWithValue("@authorPhone", authors.Phone);
                    cmd.Parameters.AddWithValue("@authorAddress", authors.Address);
                    cmd.Parameters.AddWithValue("@authorCity", authors.City);
                    cmd.Parameters.AddWithValue("@authorState", authors.State);
                    cmd.Parameters.AddWithValue("@authorZip", authors.Zip);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                    
                }
                
            }









         


        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            AuthorsModels author = new AuthorsModels();

            using (SqlConnection con = new SqlConnection(Katulong.GetCon()))
            {
                con.Open();
                string query = @"SELECT authorLN, authorFN, authorPhone, authorAddress, authorCity, authorState, authorZip FROM authors WHERE authorID=@authorID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@authorID", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.HasRows)// Record is Existing
                        {
                            while (dr.Read())
                            {
                                author.LastName = dr["authorLN"].ToString();
                                author.FirstName = dr["authorFN"].ToString();
                                author.Phone = dr["authorPhone"].ToString();
                                author.Address = dr["authorAddress"].ToString();
                                author.City = dr["authorCity"].ToString();
                                author.State = dr["authorState"].ToString();
                                author.Zip = dr["authorZip"].ToString();
                            }
                            return View(author);

                        }

                        else
                        {
                            return RedirectToAction("Index");
                        }

                    }
                }                
            }

                return View();
        }

        // POST: Authors/Edit/5
        [HttpPost]
        public ActionResult Edit(AuthorsModels author)
        {
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            using (SqlConnection con = new SqlConnection(Katulong.GetCon()))
            {
                con.Open();
                string query = @"UPDATE authors SET authorLN=@authorLN , authorFN=@authorFN, authorPhone=@authorPhone, authorAddress=@authorAddress, 
                               authorCity=@authorCity, authorState=@authorState, authorZip=@authorZip WHERE authorID=@authorID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@authorLN", author.LastName);
                    cmd.Parameters.AddWithValue("@authorFN", author.FirstName);
                    cmd.Parameters.AddWithValue("@authorPhone", author.Phone);
                    cmd.Parameters.AddWithValue("@authorAddress", author.Address);
                    cmd.Parameters.AddWithValue("@authorCity", author.City);
                    cmd.Parameters.AddWithValue("@authorState", author.State);
                    cmd.Parameters.AddWithValue("@authorZip", author.Zip);
                    cmd.Parameters.AddWithValue("@authorID", author.ID);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

            }


        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) {

                return RedirectToAction("Index");
            }
            using (SqlConnection con = new SqlConnection(Katulong.GetCon()))
            {
                con.Open();
                string query = @"DELETE FROM authors WHERE authorID=@authorID";

                using (SqlCommand cmd = new SqlCommand(query, con)){


                    cmd.Parameters.AddWithValue("@authorID", id);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
                
            }
        }

        // POST: Authors/Delete/5
        [HttpPost]
        public ActionResult Delete(AuthorsModels author)
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


        public ActionResult GenerateReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/rptAuthors.rpt"));
            rd.SetDatabaseLogon("sentonesa", "masaraptalaga03", "TAFT-CL308", "mybookstore");
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Authors Report");
            return View();
        }

        public ActionResult GenerateIndividualReport(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");



            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/rptAuthorIndividual.rpt"));
            rd.SetDatabaseLogon("sentonesa", "masaraptalaga03", "TAFT-CL308", "mybookstore");
            rd.SetParameterValue("authorID", id);
            rd.SetParameterValue("Username", "Alec Sentones");

            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Author #" + id.ToString() + "Report");
            return View();
        }

    }
}
