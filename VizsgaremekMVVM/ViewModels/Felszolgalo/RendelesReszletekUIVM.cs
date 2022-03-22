using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.Lists;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using VizsgaremekMVVM.Models.BurgerEtterem;
using System.Windows.Input;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models;

namespace VizsgaremekMVVM.ViewModels.Felszolgalo
{
    class RendelesReszletekUIVM : INotifyPropertyChanged
    {
        private HttpClientClass _http = new();
        private Rendele _rendeles;
        private Tetel? _kivalasztottTetel;
        private ObservableCollection<Tetel> _tetelek;
        public Termekek Termekek { get; set; } = new();
        public ObservableCollection<Tetel> Tetelek
        {
            get => _tetelek;
            set
            {
                _tetelek = value;
                RaisePropertyChanged();
            }
        }
        public TetelAdatok TetelAdatok { get; set; } = new();
        public Tetel? KivalasztottTetel
        {
            get => _kivalasztottTetel;
            set
            {
                _kivalasztottTetel = value;
                if (value != null)
                {
                    TetelAdatok.Burger = Termekek.Burgerek.FirstOrDefault(x=>x.Bazon == KivalasztottTetel.Bazon);
                    TetelAdatok.Ital = Termekek.Italok.FirstOrDefault(x=>x.Iazon == KivalasztottTetel.Iazon);
                    TetelAdatok.Koret = Termekek.Koretek.FirstOrDefault(x => x.Kazon == KivalasztottTetel.Kazon);
                    TetelAdatok.Desszert = Termekek.Desszertek.FirstOrDefault(x => x.Dazon == KivalasztottTetel.Dazon);
                    TetelAdatok.Bdb = KivalasztottTetel.Bdb;
                    TetelAdatok.Ddb = KivalasztottTetel.Ddb;
                    TetelAdatok.Idb = KivalasztottTetel.Idb;
                    TetelAdatok.Kdb = KivalasztottTetel.Kdb;
                    TetelAdatok.Megjegyzes = KivalasztottTetel.Megjegyzes;
                }

                RaisePropertyChanged("TetelAdatokPanelIsEnabled");

            }
        }
        public bool TetelAdatokPanelIsEnabled => KivalasztottTetel is not null;
        public ICommand TetelTorleseButton => new Button(TetelTorlese);
        public ICommand TetelFrissiteseButton => new Button(TetelFrissitese);

        /// <summary>
        /// Kap egy rendelést aminek a részleteit fogja megjeleníteni.
        /// </summary>
        /// <param name="r"></param>
        public RendelesReszletekUIVM(Rendele r)
        {
            TermekekFrissitese();
            _rendeles = r;
        }
        private async void TermekekFrissitese()
        {
            if (await Termekek.ListakFrissit("/Aktiv"))
            {
                Tetelek = new(_rendeles.Tetels);
                RaisePropertyChanged("Termekek");
            }
        }
        private async void TetelTorlese(object? o)
        {
            var tetelTorleseCall = await _http.httpClient.DeleteAsync(_http.Url + _http.Endpointok["Tetelek"]+$"/{KivalasztottTetel.Tazon}");
            if (tetelTorleseCall.IsSuccessStatusCode)
            {
                Tetelek.Remove(KivalasztottTetel);
                KivalasztottTetel = null;
            }
            else
            {
                MessageBox.Show("Hiba történt a tétel törlése közben! " + tetelTorleseCall.ReasonPhrase);
            }
        }
        private async void TetelFrissitese(object? o)
        {
            Tetel t = new()
            {
                Tazon = KivalasztottTetel.Tazon,
                Bazon = TetelAdatok.Burger.Bazon,
                Bdb = TetelAdatok.Bdb,
                Dazon = TetelAdatok.Desszert.Dazon,
                Ddb = TetelAdatok.Ddb,
                Iazon = TetelAdatok.Ital.Iazon,
                Idb = TetelAdatok.Idb,
                Kazon = TetelAdatok.Koret.Kazon,
                Kdb = TetelAdatok.Kdb,
                Megjegyzes = TetelAdatok.Megjegyzes,
                Etelstatus = KivalasztottTetel.Etelstatus,
                Italstatus = KivalasztottTetel.Italstatus,
                Razon = KivalasztottTetel.Razon,
            };
            var tetelFrissiteseCall = await _http.httpClient.PutAsync(_http.Url + _http.Endpointok["Tetelek"], _http.contentKrealas(t));
            if (tetelFrissiteseCall.IsSuccessStatusCode)
            {
                Tetel regitetel = Tetelek.First(x => x.Tazon == KivalasztottTetel.Tazon);
                regitetel.SetErtekek(t);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
