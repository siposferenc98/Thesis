using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Koret : ITermek
    {
        public Koret()
        {
            Tetels = new HashSet<Tetel>();
        }

        public int Kazon { get; set; }
        public string Knev { get; set; }
        public int Kar { get; set; }
        public string Kleir { get; set; }
        public bool? Aktiv { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tetel> Tetels { get; set; }
        public int TermekFajta => 1;

        public override string ToString()
        {
            return Knev;
        }
    }
}
