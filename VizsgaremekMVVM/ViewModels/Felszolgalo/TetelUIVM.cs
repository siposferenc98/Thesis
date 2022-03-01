using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models;
using System.Text.Json;
using System.Windows.Input;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models.Lists;
using VizsgaremekMVVM.Models.BurgerEtterem;
using System.Windows;

namespace VizsgaremekMVVM.ViewModels.Felszolgalo
{
    internal class TetelUIVM : INotifyPropertyChanged
    {
        private Rendele _rendeles;
        private HttpClientClass _http = new();
        public event EventHandler TetelHozzaadva;
        public Termekek Termekek { get; set; } = new();
        public TetelAdatok TetelAdatok { get; set; } = new();
        public ICommand TetelHozzaadasaButton => new ButtonCE(TetelHozzaadas,TetelHozzaadasCE);

        public TetelUIVM(Rendele r)
        {
            _rendeles = r;
            TermekekListakFrissit();
        }

        private async void TermekekListakFrissit()
        {
            if (await Termekek.ListakFrissit())
            {
                RaisePropertyChanged("Termekek");
            }

        }

        private async void TetelHozzaadas(object? o)
        {
            Tetel t = new()
            {
                Bazon = TetelAdatok.Burger.Bazon,
                Bdb = TetelAdatok.Bdb,
                Dazon = TetelAdatok.Desszert.Dazon,
                Ddb = TetelAdatok.Ddb,
                Iazon = TetelAdatok.Ital.Iazon,
                Idb = TetelAdatok.Idb,
                Kazon = TetelAdatok.Koret.Kazon,
                Kdb = TetelAdatok.Kdb,
                Etelstatus = 1,
                Italstatus = 1,
                Megjegyzes = TetelAdatok.Megjegyzes,
                Razon = _rendeles.Razon
            };
            var tetelHozzaadEredmeny = await _http.httpClient.PostAsync(_http.url + "Tetelek", _http.contentKrealas(t));
            if (tetelHozzaadEredmeny.IsSuccessStatusCode)
            {
                TetelHozzaadva.Invoke(this, EventArgs.Empty);
            }
            else
                MessageBox.Show("Hiba történt a tétel hozzáadása során. " + tetelHozzaadEredmeny.ReasonPhrase);

        }

        private bool TetelHozzaadasCE()
        {
            if (TetelAdatok.Burger is not null && TetelAdatok.Koret is not null && TetelAdatok.Ital is not null && TetelAdatok.Desszert is not null && TetelAdatok.Bdb is not null && TetelAdatok.Kdb is not null && TetelAdatok.Ddb is not null && TetelAdatok.Idb is not null)
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
