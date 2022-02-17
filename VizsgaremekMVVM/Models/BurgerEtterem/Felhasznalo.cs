using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{ 
    public partial class Felhasznalo
    {
        public Felhasznalo()
        {
            Foglalas = new HashSet<Foglala>();
        }

        public int Azon { get; set; }
        public string Nev { get; set; }
        public string Lak { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public int Jog { get; set; }
        public string Pw { get; set; }

        public virtual ICollection<Foglala> Foglalas { get; set; }
        public override string ToString()
        {
            return Nev;
        }
    }
}
