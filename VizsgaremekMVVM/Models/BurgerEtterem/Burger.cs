using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Media;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Burger : ITermek
    {
        public int Bazon { get; set; }
        public string Bnev { get; set; }
        public int Bar { get; set; }
        public string Bleir { get; set; }
        public bool? Aktiv { get; set; }
        public int Azon => Bazon;

        public int TermekFajta => 0;

        public string Nev => Bnev;

        public int Ar => Bar;

        public string Leiras => Bleir;

        public bool? AktivE => Aktiv;
        [JsonIgnore]
        public Brush AktivSzoveg => Aktiv == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Black) { Opacity = 0.25 };

        public override string ToString()
        {
            return Bnev;
        }
    }
}
