using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Zahtev {
        public Kupac Kupac { get; private set; }
        public Vozilo Vozilo { get; private set; }
        public DateTime DatumKreiranja { get; private set; }
        public DateTime PocetniDatum { get; private set; }
        public DateTime KrajnjiDatum { get; private set; }

        public Zahtev(Kupac kupac, Vozilo vozilo, DateTime datumKreiranja, DateTime pocetniDatum, DateTime krajnjiDatum) {
            Kupac = kupac;
            Vozilo = vozilo;
            DatumKreiranja = datumKreiranja;
            PocetniDatum = pocetniDatum;
            KrajnjiDatum = krajnjiDatum;
        }
        public override string ToString() {
            return $"Zahtev za vozilo ID: {Vozilo.Id} - Kupac: {Kupac.Ime} {Kupac.Prezime}, Datum kreiranja: {DatumKreiranja.ToShortDateString()}, Period rezervacije: {PocetniDatum.ToShortDateString()} - {KrajnjiDatum.ToShortDateString()}";
        }
    }
}
