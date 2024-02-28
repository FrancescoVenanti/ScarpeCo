using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScarpeCo.Models
{
    public class Prodotti
    {
        public int IdProdotto { get; set; }
        public string NomeProdotto { get; set; }
        public double Prezzo { get; set; }
        public string Descrizione { get; set; }
        public string ImmagineCop { get; set; }
        public string Immagine1 { get; set; }
        public string Immagine2 { get; set; }
        public bool IsVisible { get; set; }

        public Prodotti() { }

    }
}