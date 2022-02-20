using System.ComponentModel;
using VizsgaremekMVVM.Models.BurgerEtterem;
using System.Text.Json;
using System.Runtime.CompilerServices;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.Buttons;
using System.Windows.Input;
using System.Windows;
using VizsgaremekMVVM.Views;
using System.Linq;

namespace VizsgaremekMVVM.ViewModels
{
    internal class BejelentkezesVM : INotifyPropertyChanged
    {
        #region Fields
        private string _felhasznalo = string.Empty;
        private string _pw = string.Empty;
        #endregion

        private HttpClientClass _http { get; set; } = new();
        public string Felhasznalo 
        { 
            get => _felhasznalo;
            set
            {
                _felhasznalo = value;
                RaisePropertyChanged();
            }
        }
        public string Pw 
        {
            get => _pw;
            set
            {
                _pw = value;
                RaisePropertyChanged();
            }
        }

        public ICommand BejelentkezesButton => new ButtonCE(Bejelentkezes, BejelentkezesCE);
        public ICommand RegisztracioButton => new Button(RegisztracioAblak);

        public BejelentkezesVM()
        {

        }

        private void UIMegjelenit(object? window)
        {
            switch (AktivFelhasznalo.Aktiv.Jog)
            {
                case 1:
                    Window felszolgaloUI = new Views.Felszolgalo.FelszolgaloUI();
                    felszolgaloUI.Show();
                    break;
                case 2:
                    Window szakacsUI = new Views.Szakacs.SzakacsUI();
                    szakacsUI.Show();
                    break;
                case 3:
                    Window pultosUI = new Views.Pultos.PultosUI();
                    pultosUI.Show();
                    break;
                case 4:
                    Window adminUI = new Views.Admin.AdminUI();
                    adminUI.Show();
                    break;
            }

            ((Window)window!).Close();
        }

        private void RegisztracioAblak(object? o)
        {
            Regisztracio regisztracio = new();
            regisztracio.ShowDialog();
        }
        private async void Bejelentkezes(object? o)
        {
            Felhasznalo f = new() { Email = Felhasznalo , Pw = MD5Hashing.hashPW(Pw.ToString()!)};
            var result = await _http.httpClient.PostAsync(_http.url+"Felhasznalok", _http.contentKrealas(f));

            if (result.IsSuccessStatusCode)
            {
                string felhJson = await result.Content.ReadAsStringAsync();
                AktivFelhasznalo.Aktiv = JsonSerializer.Deserialize<Felhasznalo>(felhJson)!;
                AktivFelhasznalo.Token = result.Headers.GetValues("Auth").FirstOrDefault();
                
                UIMegjelenit(o);
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!");
            }
        }

        private bool BejelentkezesCE()
        {
            if (Felhasznalo.Length > 0 && Pw.Length > 0)
                return true;
            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
