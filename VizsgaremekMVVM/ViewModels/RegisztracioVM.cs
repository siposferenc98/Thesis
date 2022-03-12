using System.ComponentModel;
using System.Windows.Input;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.Buttons;
using System.Windows;
using System.Runtime.CompilerServices;
using System;
using System.Net.Http;

namespace VizsgaremekMVVM.ViewModels
{
    internal class RegisztracioVM : INotifyPropertyChanged
    {
        #region Properties
        public event EventHandler FelhasznaloModositvaVagyHozzaadva;
        private HttpClientClass _http { get; set; } = new();
        public Felhasznalo Felhasznalo { get; set; } = new();
        public string Jelszo { get; set; } = string.Empty;
        public string JelszoEllenoriz { get; set; } = string.Empty;
        public string AblakSzoveg { get; set; } = "Regisztráció";
        public ICommand RegisztralasButton => new ButtonCE(Regisztralas,RegisztralasCE);
        #endregion

        /// <summary>
        /// Regisztrációs ablak konstruktora, kapni fog egy felhasználót ami lehet null, és egy boolt ami eldönti hogy admint regisztrálunk e az ablakkal.
        /// </summary>
        /// <param name="f">Egy felhasználó amit módosítani szeretnénk vagy null ha sima regisztrálás.</param>
        /// <param name="adminRegisztracio">True ha admint szeretnénk regisztrálni</param>
        public RegisztracioVM(Felhasznalo? f, bool adminRegisztracio)
        {
            if (f is not null)
            {
                Felhasznalo.Azon = f.Azon;
                Felhasznalo.Nev = f.Nev;
                Felhasznalo.Tel = f.Tel;
                Felhasznalo.Lak = f.Lak;
                Felhasznalo.Jog = f.Jog;
                Felhasznalo.Email = f.Email;
                Felhasznalo.Pw = f.Pw;
                AblakSzoveg = "Felhasználó módosítása";
            }
            if (adminRegisztracio)
                Felhasznalo.Jog = 4;
        }

        /// <summary>
        /// Regisztrálás funkció, amennyiben admint regisztrálunk a Felhasználók/Admin API endpointhoz kapcsolódik.
        /// </summary>
        /// <param name="o"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void Regisztralas(object? o)
        {
            HttpResponseMessage eredmeny;
            if (Felhasznalo.Azon is not 0)
            {
                eredmeny = await _http.httpClient.PutAsync(_http.Url+$"{_http.Endpointok["Felhasznalok"]}/{Felhasznalo.Azon}", _http.contentKrealas(Felhasznalo));
                if (eredmeny.IsSuccessStatusCode)
                {
                    MessageBox.Show("A felhasználó módosítva lett!");
                    FelhasznaloModositvaVagyHozzaadva.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Hiba történt a felhasználó módosítása során");
                }

            }
            else
            {
                if(Jelszo == JelszoEllenoriz)
                {
                    Felhasznalo.Pw = MD5Hashing.hashPW(Jelszo);
                    eredmeny = Felhasznalo.Jog switch
                    {
                        < 4 => await _http.httpClient.PutAsync("https://localhost:5001/Felhasznalok", _http.contentKrealas(Felhasznalo)),
                        4 => await _http.httpClient.PutAsync("https://localhost:5001/Felhasznalok/Admin", _http.contentKrealas(Felhasznalo)),
                        _ => throw new NotImplementedException()
                    };

                    if(eredmeny.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Sikeres regisztrálás!");
                        FelhasznaloModositvaVagyHozzaadva.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt a regisztráció során! " + eredmeny.StatusCode);
                    }
                }
                else
                {
                    MessageBox.Show("A jelszavaknak egyeznie kell!");
                }
            }
        }
        private bool RegisztralasCE()
        {
            if (Felhasznalo.Azon is not 0)
            {
                if (Felhasznalo.Nev?.Length > 0 && Felhasznalo.Tel?.Length > 0 && Felhasznalo.Email?.Length > 0 && Felhasznalo.Lak?.Length > 0)
                    return true;
            }
            else
            {
                if (Felhasznalo.Nev?.Length > 0 && Felhasznalo.Tel?.Length > 0 && Felhasznalo.Email?.Length > 0 && Felhasznalo.Lak?.Length > 0 && Jelszo.Length > 0 && JelszoEllenoriz.Length > 0)
                    return true;
            }

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
