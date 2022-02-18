using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VizsgaremekMVVM.ViewModels;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Views
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio(Felhasznalo? f = null, bool adminRegisztracio = false)
        {
            InitializeComponent();
            RegisztracioVM regisztracioVM = new(f,adminRegisztracio);
            if (f is not null)
            {
                jelszoStackBox.Visibility = Visibility.Collapsed;
            }
            if (adminRegisztracio || f?.Jog == 4)
            {
                jogosultsag.IsEnabled = false;
            }
            DataContext = regisztracioVM;
            telSzamBox.PreviewTextInput += RegexClass.csakSzamok;
            regisztracioVM.FelhasznaloModositvaVagyHozzaadva += RegisztracioVM_FelhasznaloModositvaVagyHozzaadva;

        }

        private void RegisztracioVM_FelhasznaloModositvaVagyHozzaadva(object? sender, EventArgs e)
        {
            DialogResult = true;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if(passwordBox.Name == "jelszoEloszor")
            {
                ((RegisztracioVM)DataContext).Jelszo = passwordBox.Password;
            }
            else
            {
                ((RegisztracioVM)DataContext).JelszoEllenoriz = passwordBox.Password;
            }
        }
    }
}
