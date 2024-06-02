using CsvHelper;
using RentACar.Models;
using System.Formats.Asn1;
using System.Globalization;
using RentACar.Logic.Services;
using RentACar.Logic.Interfaces;

namespace RentACar
{
    class Program {
        static void Main(string[] args) {

            var baseUrl = "../../../Data/CsvFiles/";

            var kupci = CsvParsing.ParseKupci(baseUrl + "kupci.csv");

            /* -------------------------------- ISPIS SVIH KUPACA ------------------------- */
            /*
            foreach(var kupac in kupci) 
                Console.WriteLine(kupac.ToString());   
            */

            var vozila = CsvParsing.ParseVozila(baseUrl + "vozila.csv");

            /* -------------------------------- ISPIS SVIH VOZILA ------------------------- */
            /*
            foreach (var vozilo in vozila)
                Console.WriteLine(vozilo.ToString());
            */

            var oprema = CsvParsing.ParseOprema(baseUrl + "oprema.csv");

            /* -------------------------------- ISPIS SVE OPREME ------------------------- */
            /*
            foreach (var op in oprema)
                Console.WriteLine(op.ToString());
            */

            var vozilo_oprema = CsvParsing.ParseVoziloOprema(vozila, oprema, baseUrl + "vozilo_oprema.csv");

            /* -------------------------------- ISPIS VOZILA I OPREME ------------------------- */
            /*
            foreach (var vo in vozilo_oprema) {
                Console.Write($"VoziloId: {vo.Key} - OpremaIds: ");
                
                foreach (var opremaId in vo.Value) {
                    Console.Write($"{opremaId} ");
                }
                Console.WriteLine();
            }
            */

            var rezervacije = CsvParsing.ParseRezervacije(kupci, vozila, baseUrl + "rezervacije.csv");

            /* -------------------------------- ISPIS REZERVACIJA ------------------------- */
            /*
            Console.WriteLine("\n------------------ Rezervacije -----------------\n");
            foreach (var rez in rezervacije)
                Console.WriteLine(rez);
            */

            var zahteviZaRezervaciju = CsvParsing.ParseZahteviZaRezervaciju(kupci, vozila, baseUrl + "zahtevi_za_rezervacije.csv");

            Console.WriteLine("\n------------------------------------------------ VOZILA -----------------------------------------------------\n");
            
            foreach (var vozilo in vozila)
                Console.WriteLine(vozilo);

            Console.WriteLine("\n------------------------------------------------ KUPCI ------------------------------------------------------\n");
            
            foreach (var kupac in kupci) 
                Console.WriteLine(kupac.ToString());
            
            Console.WriteLine("\n------------------------------------------- NEUSPESNE REZERVACIJE -------------------------------------------\n");

            /* -------------------------------- ISPIS ZAHTEVA ------------------------- */
            /*
            Console.WriteLine("\n------------------ ZAHTEVI -----------------\n");
            foreach (var z in zahteviZaRezervaciju)
                Console.WriteLine(z);
            */

            IRezervisanje rezervisanjeService = new Rezervisanje(rezervacije);

            var sortiraniZahtevi = zahteviZaRezervaciju
                .OrderBy(zahtev => zahtev.DatumKreiranja)
                .ThenByDescending(zahtev => kupci.First(kupac => kupac.Id == zahtev.Kupac.Id).ClanskaKarta.TipClanskeKarte)
                .ToList();

            foreach (var zahtev in sortiraniZahtevi) {
                var kupac = kupci.FirstOrDefault(c => c.Id == zahtev.Kupac.Id);
                var vozilo = vozila.FirstOrDefault(v => v.Id == zahtev.Vozilo.Id);

                if (kupac != null && vozilo != null) {
                    rezervisanjeService.Rezervisi(kupac, vozilo, zahtev.PocetniDatum, zahtev.KrajnjiDatum);
                }
            }

            CsvParsing.SaveRezervacije(baseUrl + "nove_rezervacije.csv", rezervacije);
            Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------\n");
        }

    }
}