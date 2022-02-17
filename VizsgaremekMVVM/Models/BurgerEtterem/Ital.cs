using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{ 
    public partial class Ital : ITermek
    {
        public Ital()
        {
            Tetels = new HashSet<Tetel>();
        }

        public int Iazon { get; set; }
        public string Inev { get; set; }
        public int Iar { get; set; }
        public string Ileir { get; set; }
        public bool? Aktiv { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tetel> Tetels { get; set; }

        public int TermekFajta => 3;

        public override string ToString()
        {
            return Inev;
        }
    }
}
