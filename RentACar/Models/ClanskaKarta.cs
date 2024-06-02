using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models {
    public class ClanskaKarta {
        public TipClanskeKarte TipClanskeKarte {  get; private set; }
        public ClanskaKarta(string tipClanskeKarte) {
            if (tipClanskeKarte == "") this.TipClanskeKarte = TipClanskeKarte.None;
            else if (tipClanskeKarte == "VIP") this.TipClanskeKarte = TipClanskeKarte.VIP;
            else if (tipClanskeKarte == "Basic") this.TipClanskeKarte = TipClanskeKarte.BASIC;
        }
        public decimal dajPopust() {
            return (TipClanskeKarte == TipClanskeKarte.BASIC? 0.1m : (TipClanskeKarte == TipClanskeKarte.VIP? 0.2m : 0));
        }
    }
    public enum TipClanskeKarte {
        None,
        BASIC,
        VIP
    }
}
