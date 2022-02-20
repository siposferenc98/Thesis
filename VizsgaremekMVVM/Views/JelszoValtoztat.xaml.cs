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
using VizsgaremekMVVM.ViewModels;

namespace VizsgaremekMVVM.Views
{
    /// <summary>
    /// Interaction logic for JelszoValtoztat.xaml
    /// </summary>
    public partial class JelszoValtoztat : Window
    {
        public JelszoValtoztat()
        {
            InitializeComponent();
            DataContext = new JelszoValtoztatVM();
        }

        private void Jelszo_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pwbox = (PasswordBox)sender;
            switch (pwbox.Name)
            {
                case "aktualisJelszo":
                    ((JelszoValtoztatVM)DataContext).AktualisJelszo = pwbox.Password;
                    break;
                case "jelszoEloszor":
                    ((JelszoValtoztatVM)DataContext).UjJelszo = pwbox.Password;
                    break;
                case "jelszoMasodszor":
                    ((JelszoValtoztatVM)DataContext).UjJelszoEllenoriz = pwbox.Password;
                    break;
            }
        }
    }
}
