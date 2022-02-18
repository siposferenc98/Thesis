using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using VizsgaremekMVVM.Models.BurgerEtterem;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models;
using System.Net.Http;
using System.Windows;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    class TermekModositasVM : INotifyPropertyChanged
    {
        public event EventHandler TermekModositva;
        private HttpClientClass _http = new();
        private int _kivalasztottTermek = 0;
        private ITermek t;
        private string _nev = string.Empty;
        private int? _ar;
        private string _leiras = string.Empty;
        private bool? _aktiv = true;

        public int KivalasztottTermek 
        {
            get => _kivalasztottTermek;
            set
            {
                _kivalasztottTermek = value;
                RaisePropertyChanged();
            }
        }
        public bool KivalasztottTermekAktivE { get; set; } = true;
        public string Nev
        {
            get => _nev;
            set
            {
                _nev = value;
                RaisePropertyChanged();
            }
        }
        public int? Ar
        {
            get => _ar;
            set
            {
                _ar = value;
                RaisePropertyChanged();
            }
        }
        public string Leiras
        {
            get => _leiras;
            set
            {
                _leiras = value;
                RaisePropertyChanged();
            }
        }
        public bool? Aktiv
        {
            get => _aktiv;
            set
            {
                _aktiv = value;
                RaisePropertyChanged();
            }
        }

        public ICommand TermekFrissitButton => new ButtonCE(TermekFrissit, TermekFrissitCE);

        public TermekModositasVM(ITermek? termek = null)
        {
            if (termek is not null)
            {
                AdatokBeallit(termek);
            }
        }
        private void AdatokBeallit(ITermek termek)
        {
            t = termek;
            KivalasztottTermek = termek.TermekFajta;
            KivalasztottTermekAktivE = false;
            Nev = termek.Nev;
            Ar = termek.Ar;
            Leiras = termek.Leiras;
            Aktiv = termek.AktivE;

        }

        private async void TermekFrissit(object? o)
        {
            dynamic termek = KivalasztottTermek switch
            {
                0 => new Burger() {Bnev = Nev, Bar = (int)Ar, Bleir = Leiras, Bazon = KivalasztottTermekAktivE ? 0 : t.Azon, Aktiv = Aktiv},
                1 => new Koret() { Knev = Nev, Kar = (int)Ar, Kleir = Leiras, Kazon = KivalasztottTermekAktivE ? 0 : t.Azon, Aktiv = Aktiv },
                2 => new Desszert() { Dnev = Nev, Dar = (int)Ar, Dleir = Leiras, Dazon = KivalasztottTermekAktivE ? 0 : t.Azon, Aktiv = Aktiv },
                3 => new Ital() { Inev = Nev, Iar = (int)Ar, Ileir = Leiras, Iazon = KivalasztottTermekAktivE ? 0 : t.Azon, Aktiv = Aktiv },
                _ => throw new NotImplementedException()
            };
            string apiEndpoint = KivalasztottTermek switch
            {
                0 => "Burgerek",
                1 => "Koretek",
                2 => "Desszertek",
                3 => "Italok",
                _ => throw new NotImplementedException()
            };
            var method = KivalasztottTermekAktivE switch
            {
                false => _http.httpClient.PutAsync(_http.url + apiEndpoint,_http.contentKrealas(termek)),
                true => _http.httpClient.PostAsync(_http.url + apiEndpoint, _http.contentKrealas(termek))
            };

            HttpResponseMessage eredmeny = await method;
            if (eredmeny.IsSuccessStatusCode)
            {
                TermekModositva.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Hiba történt a termék frissítése/hozzáadása közben!");
            }
        }

        private bool TermekFrissitCE()
        {
            if (Nev.Length > 0 && Ar is not null && Leiras.Length > 0)
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
