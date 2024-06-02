using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Kupac {
        public int Id { get; }
        public string Ime { get; }
        public string Prezime { get; }
        public decimal Budzet { get; private set; }
        public ClanskaKarta ClanskaKarta { get; }

        public Kupac(int id, string ime, string prezime, decimal budzet, string tipClanskeKarte) {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            Budzet = budzet;
            ClanskaKarta = new ClanskaKarta(tipClanskeKarte);
        }

        public decimal dajPopust() {
            return ClanskaKarta?.dajPopust() ?? 0;
        }
        public bool imaNovcaZaVozilo(decimal cena) {
            return this.Budzet >= cena;
        }

        public void smanjiBudzet(decimal cena) {
            this.Budzet -= cena;
        }
        public override string ToString() {
            var tekst = this.Ime + " " + this.Prezime + " (" + ClanskaKarta.TipClanskeKarte;
            tekst += (ClanskaKarta.TipClanskeKarte != TipClanskeKarte.None) ? " " + (this.dajPopust()*100).ToString() + "%)" : ")\t";
            tekst += "\t, budzet : " + this.Budzet + "e";

            return tekst;
        }
    }
}
