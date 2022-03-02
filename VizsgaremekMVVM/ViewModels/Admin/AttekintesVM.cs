using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models.Lists;
using VizsgaremekMVVM.Views.Felszolgalo;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class AttekintesVM : INotifyPropertyChanged
    {
        private int _maiBevetel, _haviBevetel, _osszBevetel;
        private HttpClientClass _http = new();
        private Rendelesek _rendelesek = new();
        public BindingList<Rendele> AktivRendelesek { get; set; } = new();

        public event EventHandler AsztalokRajzol;
        public int MaiBevetel
        {
            get => _maiBevetel;
            set
            {
                _maiBevetel = value;
                RaisePropertyChanged();
            }
        }
        public int HaviBevetel
        {
            get => _haviBevetel;
            set
            {
                _haviBevetel = value;
                RaisePropertyChanged();
            }
        }
        public int OsszBevetel
        {
            get => _osszBevetel;
            set
            {
                _osszBevetel = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AsztalTetelekButton => new Button(AsztalTetelek);


        public AttekintesVM()
        {
            Task.Run(() => ListakFrissitAsync());
        }

        private async void ListakFrissitAsync()
        {
            while (true)
            {
                try
                {
                    if (await _rendelesek.RendelesekFrissit("/Aktiv") && await BevetelekFrissit() && Application.Current is not null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AktivRendelesek = new(_rendelesek.RendelesekLista);
                            AsztalokRajzol?.Invoke(this, new AsztalokSzinezEventArgs() { Rendelesek = AktivRendelesek });
                        });
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt az áttekintés felület frissítése közben!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                await Task.Delay(5000);
            }
        }

        private async Task<bool> BevetelekFrissit()
        {
            var bevetelekresult = await _http.httpClient.GetAsync(_http.Url + _http.Endpointok["Bevetelek"]);
            if (bevetelekresult.IsSuccessStatusCode)
            {
                List<int> bevetelek = JsonSerializer.Deserialize<List<int>>(await bevetelekresult.Content.ReadAsStringAsync())!;
                MaiBevetel = bevetelek[0];
                HaviBevetel = bevetelek[1];
                OsszBevetel = bevetelek[2];
                return true;
            }
            return false;
        }

        private void AsztalTetelek(object? o)
        {
            if (o is not null)
            {
                System.Windows.Controls.Button sender = (System.Windows.Controls.Button)o;
                Rendele? r = _rendelesek.RendelesekLista.FirstOrDefault(x => x.Asztal == int.Parse((string)sender.Tag));
                if (r is not null)
                {
                    RendelesReszletekUI rendelesReszletekUI = new(r);
                    rendelesReszletekUI.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ennél az asztalnál nem ülnek!");
                }

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
