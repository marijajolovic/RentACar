using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Logic.Interfaces;

namespace RentACar.Logic.Services
{
    public class Rezervisanje : IRezervisanje
    {
        private List<Rezervacija> _rezervacije;

        public Rezervisanje(List<Rezervacija> rezervacije)
        {
            _rezervacije = rezervacije;
        }
        private bool _daLiJeVoziloSlobodno(Vozilo vozilo, DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            bool zauzeto = _rezervacije.Any(rezervacija => rezervacija.Vozilo.Id == vozilo.Id &&                                           // isti model
                                             (rezervacija.PocetniDatum >= pocetniDatum && rezervacija.PocetniDatum <= krajnjiDatum ||                         // uslovi za preklapanje datuma
                                             pocetniDatum >= rezervacija.PocetniDatum && pocetniDatum <= rezervacija.KrajnjiDatum));
            return !zauzeto;
        }
        private bool _daLiKupacVecImaVozilo(Kupac kupac, DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            bool imaVozilo = _rezervacije.Any(rezervacija => rezervacija.Kupac.Id == kupac.Id && // isti kupac
                                             (rezervacija.PocetniDatum >= pocetniDatum && rezervacija.PocetniDatum <= krajnjiDatum ||                         // uslovi za preklapanje datuma
                                             pocetniDatum >= rezervacija.PocetniDatum && pocetniDatum <= rezervacija.KrajnjiDatum));
            return imaVozilo;
        }
        public bool Rezervisi(Kupac kupac, Vozilo vozilo, DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            if (!_daLiJeVoziloSlobodno(vozilo, pocetniDatum, krajnjiDatum)) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(kupac.Ime + " " + kupac.Prezime + " ne moze da iznajmi vozilo : " + vozilo.Marka + " " + vozilo.Model + " jer je vec zauzeto. Ne moze se iznajmiti u trazenom periodu : " + pocetniDatum.ToString("yyyy-MM-dd") + " - " + krajnjiDatum.ToString("yyyy-MM-dd"));
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            var flag = false;
            var poruka = kupac.Ime + " " + kupac.Prezime;
          
            if (_daLiKupacVecImaVozilo(kupac, pocetniDatum, krajnjiDatum))
            {
                poruka += " vec ima iznajmljeno vozilo. Ne moze se iznajmiti novo u trazenom periodu : " + pocetniDatum.ToString("yyyy-MM-dd") + " - " + krajnjiDatum.ToString("yyyy-MM-dd");
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(poruka);
                Console.ForegroundColor = ConsoleColor.Gray;
                
                flag = true;
            }
            
            var cenaPoDanu = vozilo.DajCenuPoDanu();
            var popust = kupac.dajPopust();
            var ukupnaCena = (1 - popust) * cenaPoDanu * (krajnjiDatum - pocetniDatum).Days;

            if (!kupac.imaNovcaZaVozilo(ukupnaCena))
            {
                if (flag == true) poruka = " i";
                poruka += " nema dovoljno novca za iznajmljivanje vozila : " + vozilo.Marka + " " + vozilo.Model + "(" + ukupnaCena.ToString("0.00") + "e). Raspoloziv budzet je : " + kupac.Budzet.ToString("0.00") + "e";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(poruka);
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
            if (flag == true) return false;

            _rezervacije.Add(new Rezervacija(
                kupac,
                vozilo,
                pocetniDatum,
                krajnjiDatum,
                ukupnaCena
            ));
            kupac.smanjiBudzet(ukupnaCena);

            return true;
        }

    }
}
