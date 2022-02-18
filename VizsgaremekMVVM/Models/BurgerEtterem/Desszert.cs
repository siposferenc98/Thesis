using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Media;
#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Desszert : ITermek
    {

        public int Dazon { get; set; }
        public string Dnev { get; set; }
        public int Dar { get; set; }
        public string Dleir { get; set; }
        public bool? Aktiv { get; set; }

        public int TermekFajta => 2;

        public string Nev => Dnev;

        public int Ar => Dar;

        public string Leiras => Dleir;

        public bool? AktivE => Aktiv;

        public int Azon => Dazon;
        [JsonIgnore]
        public Brush AktivSzoveg => Aktiv == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Black) { Opacity = 0.25 };

        public override string ToString()
        {
            return Dnev;
        }
    }
}
