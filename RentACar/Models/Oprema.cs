using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Oprema {
        public int Id { get; private set; }
        public string Naziv {  get; private set; }
        public int Cena {  get; private set; }
        public bool PovecavaCenuVozila { get; private set; }

        public Oprema(int id, string naziv, int cena, int povecavaCenuVozila) {
            Id = id;
            Naziv = naziv;
            Cena = cena;
            PovecavaCenuVozila = povecavaCenuVozila==1;
        }
        public override string ToString() {
            return $"Oprema: {Naziv}, Cena: {Cena}" + (PovecavaCenuVozila ? " , povecava cenu.\n" : " , smanjuje cenu.\n");
        }
    }
}
