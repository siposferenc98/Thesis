using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VizsgaremekMVVM.ViewModels;
using VizsgaremekMVVM.Models;

namespace VizsgaremekMVVM.Views
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
            DataContext = new RegisztracioVM();
            telSzamBox.PreviewTextInput += RegexClass.csakSzamok;
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
