using System.Windows.Controls;
using VizsgaremekMVVM.ViewModels.Admin;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Views.Admin
{
    /// <summary>
    /// Interaction logic for Termekek.xaml
    /// </summary>
    public partial class Termekek : UserControl
    {
        public Termekek()
        {
            InitializeComponent();
            DataContext = new TermekekVM();
        }
    }
}
