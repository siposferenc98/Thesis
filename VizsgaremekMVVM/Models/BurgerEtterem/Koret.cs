using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Media;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Koret : ITermek
    {

        public int Kazon { get; set; }
        public string Knev { get; set; }
        public int Kar { get; set; }
        public string Kleir { get; set; }
        public bool? Aktiv { get; set; }
        public int TermekFajta => 1;

        public string Nev => Knev;

        public int Ar => Kar;

        public string Leiras => Kleir;

        public bool? AktivE => Aktiv;

        public int Azon => Kazon;
        [JsonIgnore]
        public Brush AktivSzoveg => Aktiv == true ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Black) { Opacity = 0.25 };
        public override string ToString()
        {
            return Knev;
        }
    }
}
