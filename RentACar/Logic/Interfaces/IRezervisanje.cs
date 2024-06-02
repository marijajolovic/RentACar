using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Logic.Interfaces
{
    public interface IRezervisanje
    {
        public bool Rezervisi(Kupac kupac, Vozilo vozilo, DateTime pocetniDatum, DateTime krajnjiDatum);
    }
}
