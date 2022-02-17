using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models
{
    internal class AsztalokSzinezEventArgs : EventArgs
    {
        public ICollection<Rendele> Rendelesek { get; set; }
    }
}
