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
using VizsgaremekMVVM.ViewModels.Admin;

namespace VizsgaremekMVVM.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminUI.xaml
    /// </summary>
    public partial class AdminUI : Window
    {
        public AdminUI()
        {
            InitializeComponent();
            DataContext = new AdminUIVM();
            Nav(fooldalButton, null);
        }

        private void Nav(object sender, RoutedEventArgs e)
        {
            UIElementCollection gombok = NavGrid.Children;
            Button aktiv = (Button)sender;
            switch(aktiv.Name)
            {
                case "fooldalButton":
                    contentControl.Content = new Attekintes();
                    break;
                case "termekekButton":
                    contentControl.Content = new Termekek();
                    break;
                case "felhasznalokNav":
                    contentControl.Content = new Felhasznalok();
                    break; 
            }
            foreach (object gomb in gombok)
            {
                Button button = (Button)gomb;
                if (aktiv.Name == button.Name)
                {
                    button.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    button.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }
    }
}
