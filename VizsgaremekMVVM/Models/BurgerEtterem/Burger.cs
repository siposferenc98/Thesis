using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Burger : ITermek
    {
        public Burger()
        {
            Tetels = new HashSet<Tetel>();
        }

        public int Bazon { get; set; }
        public string Bnev { get; set; }
        public int Bar { get; set; }
        public string Bleir { get; set; }
        public bool? Aktiv { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tetel> Tetels { get; set; }

        public int TermekFajta => 0;

        public override string ToString()
        {
            return Bnev;
        }
    }
}
