using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models.Buttons;
using System.Windows.Input;
using VizsgaremekMVVM.Views.Felszolgalo;
using VizsgaremekMVVM.Models.Lists;

namespace VizsgaremekMVVM.ViewModels.Felszolgalo
{
    internal class FelszolgaloUIVM : INotifyPropertyChanged
    {
        #region Private Fields
        private int _kivalasztottAsztal;
        private bool _beteroVendeg;
        private readonly HttpClientClass _http = new();
        private Rendelesek _rendelesek = new();
        private Foglalasok _foglalasok = new();
        #endregion
        public event EventHandler AsztalokRajzol;

        #region DataBinding
        public BindingList<Rendele> Rendelesek { get; set; } = new();
        public ObservableCollection<Foglala> Foglalasok { get; set; } = new();
        public ObservableCollection<int> Asztalok { get; set; } = new();
        public int KivalasztottAsztal
        {
            get => _kivalasztottAsztal;
            set
            {
                _kivalasztottAsztal = value;
                RaisePropertyChanged();
            }
        }
        public Rendele? KivalasztottRendeles { get; set; }
        public Foglala? KivalasztottFoglalas { get; set; }
        public bool BeteroVendeg
        {
            get => _beteroVendeg;
            set
            {
                _beteroVendeg = value;
                RaisePropertyChanged("FoglalasIsEnabled");
            }
        }
        public bool FoglalasIsEnabled => !BeteroVendeg;

        public ICommand RendelesFelveteleButton => new ButtonCE(RendelesHozzaad, RendelesHozzaadCE);
        public ICommand TetelFelvetelButton => new ButtonCE(TetelHozzaad,RendelesKivalasztvaCE);
        public ICommand RendelesTorleseButton => new ButtonCE(RendelesTorles, RendelesKivalasztvaCE);
        public ICommand RendelesReszletekButton => new ButtonCE(RendelesReszlete, RendelesKivalasztvaCE);
        public ICommand RendelesFizetesreVarButton => new ButtonCE(RendelesFizetesreVar, RendelesKivalasztvaCE);
        public ICommand RendelesFizetveButton => new ButtonCE(RendelesFizetve, RendelesKivalasztvaCE);
        public ICommand AsztalTetelekButton => new Button(AsztalTetelek);

        #endregion


        public FelszolgaloUIVM()
        {
            Task.Run(() => ListakFrissitAsync());
            Rendelesek.ListChanged += Rendelesek_ListChanged;
        }

        private void Rendelesek_ListChanged(object sender, ListChangedEventArgs e)
        {
            AsztalokRajzol.Invoke(this, new AsztalokSzinezEventArgs() { Rendelesek = Rendelesek });
        }

        private async void ListakFrissitAsync()
        {
            while (true)
            {
                try
                {
                    if (await _rendelesek.RendelesekFrissit("/Aktiv") && await _foglalasok.FoglalasokFrissit("/Aktiv") && Application.Current is not null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            RendelesekFrissit();
                            FoglalasokFrissit();
                            AsztalokFrissit();
                        });
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

        #region Api

        private async void RendelesHozzaad(object? o)
        {
            Rendele r;
            if(BeteroVendeg)
            {
                Foglala f = new()
                {
                    Azon = 0,
                    Foglalasido = DateTime.Now,
                    Leadva = DateTime.Now,
                    Szemelydb = 1,
                    Megjelent = true,
                };

                var vendegFoglalasHozzaadEredmeny = await _http.httpClient.PostAsync(_http.url + "Foglalasok", _http.contentKrealas(f));
                if(vendegFoglalasHozzaadEredmeny.IsSuccessStatusCode)
                {
                    f = JsonSerializer.Deserialize<Foglala>(await vendegFoglalasHozzaadEredmeny.Content.ReadAsStringAsync())!;
                }

                r = new()
                {
                    Fazon = f.Fazon,
                    Etelstatus = 1,
                    Italstatus = 1,
                    Ido = DateTime.Now,
                    Asztal = KivalasztottAsztal
                };
            }
            else
            {
                r = new()
                {
                    Fazon = KivalasztottFoglalas.Fazon,
                    Etelstatus = 1,
                    Italstatus = 1,
                    Ido = DateTime.Now,
                    Asztal = KivalasztottAsztal,
                };
            }
            var rendelesHozzaadEredmeny = await _http.httpClient.PostAsync(_http.url+"Rendelesek",_http.contentKrealas(r));
            if (rendelesHozzaadEredmeny.IsSuccessStatusCode)
            {
                r = JsonSerializer.Deserialize<Rendele>(await rendelesHozzaadEredmeny.Content.ReadAsStringAsync())!;
                Rendelesek.Add(r);
                KivalasztottAsztal = -1;
            }

        }

        private async void RendelesTorles(object? o)
        {
            var rendelesTorlesEredmeny = await _http.httpClient.DeleteAsync(_http.url + $"Rendelesek/{KivalasztottRendeles.Razon}");
            if (rendelesTorlesEredmeny.IsSuccessStatusCode)
            {
                Rendelesek.Remove(KivalasztottRendeles);
                CommandManager.InvalidateRequerySuggested();
            }
        }
        private async void RendelesFizetesreVar(object? o)
        {
            KivalasztottRendeles.Etelstatus = 3;
            KivalasztottRendeles.Italstatus = 3;
            var rendelesFizetesreVarEredmeny = await _http.httpClient.PutAsync(_http.url + "Rendelesek", _http.contentKrealas(KivalasztottRendeles));
        }
        private async void RendelesFizetve(object? o)
        {
            KivalasztottRendeles.Etelstatus = 4;
            KivalasztottRendeles.Italstatus = 4;
            var rendelesFizetesreVarEredmeny = await _http.httpClient.PutAsync(_http.url + "Rendelesek", _http.contentKrealas(KivalasztottRendeles));
        }
        #endregion

        #region Frissitesek
        private void RendelesekFrissit()
        {
            _rendelesek.RendelesekLista.ForEach(x =>
            {

                if (!Rendelesek.Any(r=>r.Razon == x.Razon))
                {
                    Rendelesek.Add(x);
                }
                else
                {
                    Rendele r = Rendelesek.First(r => r.Razon == x.Razon);
                    r.Etelstatus = x.Etelstatus;
                    r.Italstatus = x.Italstatus;
                    r.Tetels = x.Tetels;
                } 
                
            });

            RendelesekTorlese();
           
        }
        private void FoglalasokFrissit()
        {

            _foglalasok.FoglalasokLista.ForEach(x =>
            {
                if (!Foglalasok.Any(f => f.Fazon == x.Fazon))
                {
                    Foglalasok.Add(x);
                }
            });

            FoglalasokTorlese();
        }
        private void AsztalokFrissit()
        {

            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                if (!_rendelesek.RendelesekLista.Any(r => r.Asztal == x && r.Etelstatus < 4 && r.Italstatus < 4) && !Asztalok.Contains(x))
                {
                    Asztalok.Add(x);
                    Asztalok.Sort();
                }
            });
            AsztalokTorlese();
            
        }
        #endregion

        #region Torlesek
        private void RendelesekTorlese()
        {
            List<Rendele> torlesre = Rendelesek.Where(x => !_rendelesek.RendelesekLista.Any(o => x.Razon == o.Razon)).ToList();
            torlesre.AddRange(Rendelesek.Where(x => x.Etelstatus == 4 || x.Italstatus == 4));

            if (torlesre.Contains(KivalasztottRendeles))
                KivalasztottRendeles = null;
            torlesre.ForEach(x => Rendelesek.Remove(x));
        }

        private void FoglalasokTorlese()
        {
            List<Foglala> torlesre = Foglalasok.Where(x => !_foglalasok.FoglalasokLista.Any(o => x.Fazon == o.Fazon)).ToList();

            if (torlesre.Contains(KivalasztottFoglalas))
                KivalasztottFoglalas = null;
            torlesre.ForEach(x => Foglalasok.Remove(x));
        }
        private void AsztalokTorlese()
        {
            List<int> torlesre = Asztalok.Where(x => _rendelesek.RendelesekLista.Any(o => o.Asztal == x && o.Etelstatus < 4 && o.Italstatus < 4)).ToList();
            
            if(torlesre.Contains(KivalasztottAsztal))
                KivalasztottAsztal = -1;
            torlesre.ForEach(x=> Asztalok.Remove(x));
        }
        #endregion

        private void TetelHozzaad(object? o)
        {
            Window tetelUI = new TetelUI(KivalasztottRendeles);
            tetelUI.ShowDialog();
        }
        private void RendelesReszlete(object? o)
        {
            RendelesReszletekUI rendelesReszletekUI = new(KivalasztottRendeles);
            rendelesReszletekUI.ShowDialog();
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

        #region CanExecute
        private bool RendelesHozzaadCE()
        {
            if (KivalasztottAsztal is not 0 && (KivalasztottFoglalas is not null || BeteroVendeg))
                return true;
            return false;
        }
        private bool RendelesKivalasztvaCE()
        {
            return KivalasztottRendeles is not null;
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
