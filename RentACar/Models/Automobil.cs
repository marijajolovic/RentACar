using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Automobil : Vozilo{
        public int Kilometraza { get; private set; }
        public TipKaroserije TipKaroserije {  get; private set; }
        public  List<Oprema> Oprema {  get; private set; }

        public Automobil(int id, string model, string marka, decimal potrosnja, int kilometraza, TipKaroserije tipKaroserije) {
            base.Id = id;
            base.Model = model;
            base.Marka = marka;
            base.Potrosnja = potrosnja;
            Kilometraza = kilometraza;
            Oprema = new List<Oprema>();
            TipKaroserije = tipKaroserije;
            //base.Cena = 200;
        }
        public override decimal DajCenuPoDanu() {
            base.Cena = 200;
            
            if (base.Marka == "Mercedes") {                
                if (this.Kilometraza < 50000) base.Cena += 0.6m * base.Cena;
                if (this.TipKaroserije == TipKaroserije.Limuzina) base.Cena += 0.7m * base.Cena;
                if (this.TipKaroserije == TipKaroserije.Hecbek && this.Kilometraza > 100000) base.Cena -= 0.3m * base.Cena;
            }
            else if (base.Marka == "BMW") {
                if (base.Potrosnja < 7) base.Cena += 0.15m * base.Cena;
                else if (base.Potrosnja > 7) base.Cena -= 0.10m * base.Cena;
                else base.Cena -= 0.15m * base.Potrosnja;
            }
            else if (base.Marka == "Peugeot") {
                if (this.TipKaroserije == TipKaroserije.Limuzina) base.Cena += 0.15m * base.Cena;
                else if (this.TipKaroserije == TipKaroserije.Karavan) base.Cena += 0.2m * base.Cena;
                else base.Cena -= 0.05m * base.Cena;
            }

            foreach(var oprema in Oprema) {
                if (oprema.PovecavaCenuVozila) base.Cena += oprema.Cena;
                else base.Cena -= oprema.Cena;
            }

            return base.Cena;
        }

        public override string ToString() {
            var tekst =  base.ToString() + $"Kilometraza: {Kilometraza} km, Tip Karoserije: {TipKaroserije}\n";
            foreach(var op in Oprema) {
                tekst += op.ToString() + "\n";
            }
            return tekst;
        }
    }
    public enum TipKaroserije {
        Limuzina,
        Hecbek,
        Karavan,
        Kupe,
        Kabriolet,
        Minivan, 
        SUV,
        Pickup
    }
}
