using System.ComponentModel;
using System.Windows.Input;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.Buttons;
using System.Windows;
using System.Runtime.CompilerServices;

namespace VizsgaremekMVVM.ViewModels
{
    internal class RegisztracioVM : INotifyPropertyChanged
    {
        private HttpClientClass _http { get; set; } = new();
        public Felhasznalo Felhasznalo { get; set; } = new();
        public string Jelszo { get; set; } = string.Empty;
        public string JelszoEllenoriz { get; set; } = string.Empty;

        public ICommand RegisztralasButton => new ButtonCE(Regisztralas,RegisztralasCE);

        private void Regisztralas(object? o)
        {
            if(Jelszo == JelszoEllenoriz)
            {
                Felhasznalo.Pw = MD5Hashing.hashPW(Jelszo);
                var eredmeny = _http.httpClient.PutAsync("https://localhost:5001/Felhasznalok", _http.contentKrealas(Felhasznalo));

                if(eredmeny.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sikeres regisztrálás!");
                    ((Window)o!).Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a regisztráció során! " + eredmeny.Result.StatusCode);
                }
            }
            else
            {
                MessageBox.Show("A jelszavaknak egyeznie kell!");
            }
        }
        private bool RegisztralasCE()
        {
            if (Felhasznalo.Nev?.Length > 0 && Felhasznalo.Tel?.Length > 0 && Felhasznalo.Email?.Length > 0 && Felhasznalo.Lak?.Length > 0 && Jelszo.Length > 0 && JelszoEllenoriz.Length > 0)
                return true;

            return false;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
