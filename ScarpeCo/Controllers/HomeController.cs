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
                //string query = "SELECT * FROM Prodotti WHERE IsVisible = 1";
                string query = "SELECT * FROM Prodotti";
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
                string query = "UPDATE Prodotti SET IsVisible = CASE WHEN IsVisible = 0 THEN 1 ELSE 0 END WHERE IdProdotto = " + id;
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

        public ActionResult Create()
        {
            ViewBag.Message = "Inserisci elemento";

            return View();
        }
        [HttpPost]
        public ActionResult Create(Prodotti prodotto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Prodotti (NomeProdotto, Prezzo, Descrizione, ImmagineCop, Immagine1, Immagine2, IsVisible) VALUES (@NomeProdotto, @Prezzo, @Descrizione, @ImmagineCop, @Immagine1, @Immagine2, @IsVisible)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
                cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
                cmd.Parameters.AddWithValue("@Descrizione", prodotto.Descrizione);
                cmd.Parameters.AddWithValue("@ImmagineCop", prodotto.ImmagineCop);
                cmd.Parameters.AddWithValue("@Immagine1", prodotto.Immagine1);
                cmd.Parameters.AddWithValue("@Immagine2", prodotto.Immagine2);
                cmd.Parameters.AddWithValue("@IsVisible", prodotto.IsVisible);
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

        
    }
}