using ScarpeCo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScarpeCo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Prodotti> prodotti = new List<Prodotti>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Prodotti WHERE IsVisible = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Prodotti prodotto = new Prodotti();
                    prodotto.IdProdotto = Convert.ToInt32(reader["IdProdotto"]);
                    prodotto.NomeProdotto = reader["NomeProdotto"].ToString();
                    prodotto.Prezzo = Convert.ToDouble(reader["Prezzo"]);
                    prodotto.Descrizione = reader["Descrizione"].ToString();
                    prodotto.ImmagineCop = reader["ImmagineCop"].ToString();
                    prodotto.Immagine1 = reader["Immagine1"].ToString();
                    prodotto.Immagine2 = reader["Immagine2"].ToString();
                    prodotto.IsVisible = Convert.ToBoolean(reader["IsVisible"]);
                    prodotti.Add(prodotto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return View(prodotti);
        }

        
        public ActionResult DettaglioProdotto(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            Prodotti prodotto = new Prodotti();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Prodotti WHERE IdProdotto = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    prodotto.IdProdotto = Convert.ToInt32(reader["IdProdotto"]);
                    prodotto.NomeProdotto = reader["NomeProdotto"].ToString();
                    prodotto.Prezzo = Convert.ToDouble(reader["Prezzo"]);
                    prodotto.Descrizione = reader["Descrizione"].ToString();
                    prodotto.ImmagineCop = reader["ImmagineCop"].ToString();
                    prodotto.Immagine1 = reader["Immagine1"].ToString();
                    prodotto.Immagine2 = reader["Immagine2"].ToString();
                    prodotto.IsVisible = Convert.ToBoolean(reader["IsVisible"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return View(prodotto);
        }

        public ActionResult DeleteProduct(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "UPDATE Prodotti SET IsVisible = 0 WHERE IdProdotto = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}