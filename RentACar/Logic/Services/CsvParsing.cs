using CsvHelper;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;

namespace RentACar.Logic.Services
{
    public class CsvParsing
    {
        /* ----------------------------------------------- VOZILA ----------------------------------------------------- */
        public static List<Vozilo> ParseVozila(string csvFajl)
        {
            List<Vozilo> vozila = new List<Vozilo>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {

                    var tipVozila = csv.GetField<string>("TipVozila");

                    if (tipVozila == "Automobil")
                    {
                        var automobil = new Automobil(
                            csv.GetField<int>("Id"),
                            csv.GetField<string>("Model"),
                            csv.GetField<string>("Marka"),
                            csv.GetField<decimal>("Potrosnja"),
                            csv.GetField<int>("Kilometraza"),
                            (TipKaroserije)Enum.Parse(typeof(TipKaroserije), csv.GetField<string>("Tip"))
                        );
                        vozila.Add(automobil);
                    }
                    else if (tipVozila == "Motor")
                    {
                        var motor = new Motor(
                            csv.GetField<int>("Id"),
                            csv.GetField<string>("Model"),
                            csv.GetField<string>("Marka"),
                            csv.GetField<decimal>("Potrosnja"),
                            csv.GetField<int>("Kubikaza"),
                            csv.GetField<int>("Snaga"),
                            (TipMotora)Enum.Parse(typeof(TipMotora), csv.GetField<string>("Tip"))
                        );
                        vozila.Add(motor);
                    }
                }
            }
            return vozila;
        }
        /* ----------------------------------------------- KUPCI ----------------------------------------------------- */
        public static List<Kupac> ParseKupci(string csvFajl)
        {
            List<Kupac> kupci = new List<Kupac>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var kupac = new Kupac(
                        csv.GetField<int>("Id"),
                        csv.GetField<string>("Ime"),
                        csv.GetField<string>("Prezime"),
                        csv.GetField<decimal>("Budzet"),
                        csv.GetField<string>("Clanarina")
                    );

                    kupci.Add(kupac);
                }
            }
            return kupci;

        }
        /* ----------------------------------------------- OPREMA ----------------------------------------------------- */
        public static List<Oprema> ParseOprema(string csvFajl)
        {
            List<Oprema> oprema = new List<Oprema>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var op = new Oprema(
                        csv.GetField<int>("Id"),
                        csv.GetField<string>("Naziv"),
                        csv.GetField<int>("Cena"),
                        csv.GetField<int>("PovecavaCenu")
                    );

                    oprema.Add(op);
                }
            }

            return oprema;
        }

        /* ----------------------------------------------- OPREMA ZA VOZILA ----------------------------------------------------- */
        public static Dictionary<int, List<int>> ParseVoziloOprema(List<Vozilo> vozila, List<Oprema> oprema, string csvFajl)
        {
            var vozilo_oprema = new Dictionary<int, List<int>>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var voziloId = csv.GetField<int>("VoziloId");
                    var opremaId = csv.GetField<int>("OpremaId");

                    if (!vozilo_oprema.ContainsKey(voziloId))
                    {
                        vozilo_oprema[voziloId] = new List<int>();
                    }
                    vozilo_oprema[voziloId].Add(opremaId);
                }
            }

            foreach (var vozilo in vozila)
            {
                if (vozilo is Automobil)
                {
                    if (vozilo_oprema.TryGetValue(vozilo.Id, out var opremaIds))
                    {
                        foreach (var opremaId in opremaIds)
                        {
                            var op = oprema.Find(o => o.Id == opremaId);
                            if (op != null)
                            {
                                ((Automobil)vozilo).Oprema.Add(op);
                            }
                        }
                    }
                }

            }

            return vozilo_oprema;
        }

        /* ----------------------------------------------- REZERVACIJE ----------------------------------------------------- */
        public static List<Rezervacija> ParseRezervacije(List<Kupac> kupci, List<Vozilo> vozila, string csvFajl)
        {
            var rezervacije = new List<Rezervacija>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var rezervacija = new Rezervacija(
                        kupci.Find(k => k.Id == csv.GetField<int>("KupacId")),
                        vozila.Find(v => v.Id == csv.GetField<int>("VoziloId")),
                        csv.GetField<DateTime>("PocetakRezervacije"),
                        csv.GetField<DateTime>("KrajRezervacije"),
                        null
                    );

                    rezervacije.Add(rezervacija);
                }
            }

            return rezervacije;
        }
        /* ----------------------------------------------- ZAHTEVI ----------------------------------------------------- */
        public static List<Zahtev> ParseZahteviZaRezervaciju(List<Kupac> kupci, List<Vozilo> vozila, string csvFajl)
        {
            var zahtevi = new List<Zahtev>();

            using (var reader = new StreamReader(csvFajl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var kupacId = csv.GetField<int>("KupacId");
                    var voziloId = csv.GetField<int>("VoziloId");
                    var datumKreiranja = csv.GetField<DateTime>("DatumDolaska");
                    var pocetniDatum = csv.GetField<DateTime>("PocetakRezervacije");
                    var krajnjiDatum = pocetniDatum.AddDays(csv.GetField<int>("BrojDana"));

                    var kupac = kupci.Find(k => k.Id == kupacId);
                    var vozilo = vozila.Find(v => v.Id == voziloId);

                    var zahtev = new Zahtev
                    (
                        kupac,
                        vozilo,
                        datumKreiranja,
                        pocetniDatum,
                        krajnjiDatum
                    );

                    zahtevi.Add(zahtev);
                }
            }

            return zahtevi;
        }
        /* ----------------------------------------------- NOVI FAJL ----------------------------------------------------- */
        public static void SaveRezervacije(string csvFajl, List<Rezervacija> rezervacije)
        {

            using (var writer = new StreamWriter(csvFajl))
            {
                writer.WriteLine("VoziloId,KupacId,PocetakRezervacije,KrajRezervacije");
                foreach (var rezervacija in rezervacije)
                {
                    if (rezervacija.UkupnaCena == 0) continue;
                    var line = $"{rezervacija.Vozilo.Id},{rezervacija.Kupac.Id},{rezervacija.PocetniDatum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)},{rezervacija.KrajnjiDatum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}

