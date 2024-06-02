using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public abstract class Vozilo {
        public int Id {  get; protected set; }
        public string Model { get; protected set; }
        public string Marka {  get; protected set; }
        public decimal Potrosnja { get; protected set; }
        public decimal Cena {  get; protected set; }

        public abstract decimal DajCenuPoDanu();

        public override string ToString() {
            return $"{Marka} {Model}, Potrosnja : {Potrosnja} l/100km, Cena : {this.DajCenuPoDanu().ToString("0.00")} e po danu\n";
        }
    }
}
