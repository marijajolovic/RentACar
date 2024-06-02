using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class Rezervacija {
        public Kupac Kupac { get; private set; }
        public Vozilo Vozilo { get; private set; }
        public DateTime PocetniDatum { get; private set; }
        public DateTime KrajnjiDatum {  get; private set; }
        public decimal UkupnaCena {  get; private set; }

        public Rezervacija(Kupac kupac, Vozilo vozilo, DateTime pocetniDatum, DateTime krajnjiDatum, decimal? ukupnaCena) {
            Kupac = kupac;
            Vozilo = vozilo;
            PocetniDatum = pocetniDatum;
            KrajnjiDatum = krajnjiDatum;
            UkupnaCena = ukupnaCena ?? 0;
        }
        public override string ToString() {
            return $"Rezervacija za vozilo: {Vozilo.Id}, Kupac: {Kupac.Id}, Period: {PocetniDatum.ToShortDateString()} - {KrajnjiDatum.ToShortDateString()}, Ukupna cena: {UkupnaCena}";
        }
    }
}
