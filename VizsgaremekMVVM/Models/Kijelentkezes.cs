using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VizsgaremekMVVM.Views;

namespace VizsgaremekMVVM.Models
{
    internal class Kijelentkezes
    {
        public static void kijelentkezes(object sender, RoutedEventArgs e)
        {
            AktivFelhasznalo.Aktiv= null;
            AktivFelhasznalo.Token= null;
            Window bejelentkezes = new Bejelentkezes();
            bejelentkezes.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != bejelentkezes)
                {
                    window.Close();
                }
            }
        }
    }
}
