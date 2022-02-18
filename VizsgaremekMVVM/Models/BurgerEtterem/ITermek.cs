using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public interface ITermek
    {
        public int TermekFajta { get; }
        public int Azon { get; }
        public string Nev { get; }
        public int Ar { get; }
        public string Leiras { get; }
        public bool? AktivE { get; }
    }
}
