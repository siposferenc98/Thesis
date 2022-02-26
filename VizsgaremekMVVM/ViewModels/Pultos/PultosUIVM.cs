using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models.Lists;

namespace VizsgaremekMVVM.ViewModels.Pultos
{
    internal class PultosUIVM : INotifyPropertyChanged
    {
        private HttpClientClass _http = new();
        private Tetelek _tetelek = new();
        public BindingList<Tetel> Tetelek { get; set; } = new();
        public Tetel? KivalasztottTetel { get; set; }
        public ICommand TetelKeszButton => new ButtonCE(TetelKesz, TetelKeszCE);
        public PultosUIVM()
        {
            Task.Run(() => ListakFrissitAsync());
        }

        private async void ListakFrissitAsync()
        {
            while (true)
            {
                try
                {
                    if (await _tetelek.TetelekFrissit("/Pultos") && Application.Current is not null)
                        Application.Current.Dispatcher?.Invoke(() =>
                        {
                            TetelekListaFrissit();
                        });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                await Task.Delay(5000);
            }
        }

        private void TetelekListaFrissit()
        {
            _tetelek.TetelLista.ForEach(t =>
            {
                if (!Tetelek.Any(x => x.Tazon == t.Tazon))
                {
                    Tetelek.Add(t);
                }
                else
                {
                    Tetel akttetel = Tetelek.First(x => t.Tazon == x.Tazon);
                    akttetel.SetErtekek(t);
                }
            });

            TetelekTorlese();
        }

        private void TetelekTorlese()
        {
            List<Tetel> torlesre = Tetelek.Where(x => !_tetelek.TetelLista.Any(o => x.Tazon == o.Tazon)).ToList();
            torlesre.AddRange(Tetelek.Where(x => x.Etelstatus == 4 || x.Italstatus == 4));

            if (torlesre.Contains(KivalasztottTetel))
                KivalasztottTetel = null;
            torlesre.ForEach(x => Tetelek.Remove(x));
        }

        private async void TetelKesz(object? o)
        {
            KivalasztottTetel.Italstatus = 2;
            var tetelKeszCall = await _http.httpClient.PutAsync(_http.url + "Tetelek/Status", _http.contentKrealas(KivalasztottTetel));
            if (tetelKeszCall.IsSuccessStatusCode)
            {
                Tetelek.Remove(KivalasztottTetel);
            }
        }
        private bool TetelKeszCE()
        {
            return KivalasztottTetel is not null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
