using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{ 
    public partial class Foglala
    {
        
        public Foglala()
        {
            Rendeles = new HashSet<Rendele>();
        }

        public int Fazon { get; set; }
        public int? Azon { get; set; }
        public int Szemelydb { get; set; }
        public DateTime Foglalasido { get; set; }
        public DateTime Leadva { get; set; }
        public bool Megjelent { get; set; }

        [JsonIgnore]
        public virtual Felhasznalo AzonNavigation { get; set; }
        public virtual ICollection<Rendele> Rendeles { get; set; }

        [NotMapped]
        public string FelhasznaloNev { get; set; }

        public override string ToString()
        {
            return $"Név: {FelhasznaloNev}, {Szemelydb} főre, {Foglalasido.Year}.{Foglalasido.Month}.{Foglalasido.Day} {Foglalasido.TimeOfDay}. F.sz: {Fazon}.";
        }
    }
}
