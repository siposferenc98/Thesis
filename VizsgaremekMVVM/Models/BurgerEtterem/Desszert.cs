using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using VizsgaremekMVVM.Models.BurgerEtterem;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Desszert : ITermek
    {
        public Desszert()
        {
            Tetels = new HashSet<Tetel>();
        }

        public int Dazon { get; set; }
        public string Dnev { get; set; }
        public int Dar { get; set; }
        public string Dleir { get; set; }
        public bool? Aktiv { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tetel> Tetels { get; set; }

        public int TermekFajta => 2;

        public override string ToString()
        {
            return Dnev;
        }
    }
}
