using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Media;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{ 
    public partial class Ital : ITermek
    {

        public int Iazon { get; set; }
        public string Inev { get; set; }
        public int Iar { get; set; }
        public string Ileir { get; set; }
        public bool? Aktiv { get; set; }

        public int TermekFajta => 3;

        public string Nev => Inev;

        public int Ar => Iar;

        public string Leiras => Ileir;

        public bool? AktivE => Aktiv;

        public int Azon => Iazon;
        [JsonIgnore]
        public Brush AktivSzoveg => Aktiv == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Black) { Opacity = 0.25 };
        public override string ToString()
        {
            return Inev;
        }
    }
}
