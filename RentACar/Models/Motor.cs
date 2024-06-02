using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Motor : Vozilo {

        public int Kubikaza { get; private set; }
        public int Snaga { get; private set; }
        public TipMotora Tip { get; private set; }
        public Motor(int id, string model, string marka, decimal potrosnja, int kubikaza, int snaga, TipMotora tipMotora) {
            base.Id = id;
            base.Model = model;
            base.Marka = marka;
            base.Potrosnja = potrosnja;
            Kubikaza = kubikaza;
            Snaga = snaga;
            Tip = tipMotora;
            //base.Cena = 100;
        }
        public override decimal DajCenuPoDanu() {
            base.Cena = 100;

            if (base.Marka == "Harley") {
                base.Cena += 0.15m * base.Cena;
                if (this.Kubikaza > 1200) base.Cena += 0.10m * base.Cena;
                else base.Cena -= 0.05m * base.Cena;
            }
            else if (base.Marka == "Yamaha") {
                base.Cena += 0.10m * base.Cena;

                if (this.Snaga > 180) base.Cena += 0.05m * base.Cena;
                else base.Cena -= 0.10m * base.Cena;

                if (this.Tip == TipMotora.Heritage) base.Cena += 50;
                else if (this.Tip == TipMotora.Sport) base.Cena += 100;
                else base.Cena -= 10;
            }
            return base.Cena;
        }

        public override string ToString() {
            return base.ToString() + $"Kubikaza: {Kubikaza} kubika, Snaga: {Snaga} ks, Tip motora: {Tip}\n";
        }
    }

    public enum TipMotora {
        Adventure,
        Heritage,
        Tour,
        Roadster,
        UrbanMobility,
        Sport
    }
}
