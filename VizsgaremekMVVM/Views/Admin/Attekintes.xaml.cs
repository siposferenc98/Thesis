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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.ViewModels.Admin;

namespace VizsgaremekMVVM.Views.Admin
{
    /// <summary>
    /// Interaction logic for Attekintes.xaml
    /// </summary>
    public partial class Attekintes : UserControl
    {
        public Attekintes()
        {
            InitializeComponent();
            AttekintesVM attekintesVM = new();
            DataContext = attekintesVM;
            attekintesVM.AsztalokRajzol += asztalokSzinez;
        }
        private void asztalokSzinez(object? sender, EventArgs e)
        {
            AsztalokSzinezEventArgs args = (AsztalokSzinezEventArgs)e;
            UIElementCollection asztalok = AsztalokGrid.Children;
            foreach (object asztal in asztalok)
            {
                Button a = (Button)asztal;
                if (args.Rendelesek.Any(x => x.Asztal == int.Parse((string)a.Tag)))
                    a.Background = new SolidColorBrush(Colors.Teal);
                else
                    a.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

    }
}
