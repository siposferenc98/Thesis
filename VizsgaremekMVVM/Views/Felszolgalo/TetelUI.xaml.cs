using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.ViewModels.Felszolgalo;
using VizsgaremekMVVM.Models;

namespace VizsgaremekMVVM.Views.Felszolgalo
{
    /// <summary>
    /// Interaction logic for TetelUI.xaml
    /// </summary>
    public partial class TetelUI : Window
    {
        public TetelUI(Rendele r)
        {
            InitializeComponent();
            TetelUIVM tetelUIVM = new(r);
            DataContext = tetelUIVM;
            Bdb.PreviewTextInput += RegexClass.csakSzamok;
            Idb.PreviewTextInput += RegexClass.csakSzamok;
            Ddb.PreviewTextInput += RegexClass.csakSzamok;
            Kdb.PreviewTextInput += RegexClass.csakSzamok;
            tetelUIVM.TetelHozzaadva += TetelUIVM_TetelHozzaadva;
        }

        private void TetelUIVM_TetelHozzaadva(object? sender, EventArgs e)
        {
            DialogResult = true;
        }
    }
}
