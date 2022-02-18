using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models.Lists;
using System.Windows;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class FoglalasokVM : INotifyPropertyChanged
    {
        private string _filterSzoveg = string.Empty;
        private int _filterIdo;
        private Foglalasok _foglalasokLista = new();
        private ObservableCollection<Foglala> _foglalasok;

        public ObservableCollection<Foglala> Foglalasok
        {
            get => _foglalasok;
            set
            {
                _foglalasok = value;
                RaisePropertyChanged();
            }
        }
        public string FilterSzoveg
        {
            get => _filterSzoveg;
            set
            {
                _filterSzoveg = value;
                Filtereles();
                RaisePropertyChanged();
            }
        }
        public int FilterIdo
        {
            get => _filterIdo;
            set
            {
                _filterIdo = value;
                Filtereles();
                RaisePropertyChanged();
            }
        }

        public FoglalasokVM()
        {
            FoglalasokFrissit();
        }
        private async void FoglalasokFrissit()
        {
            if (await _foglalasokLista.FoglalasokFrissit())
            {
                Foglalasok = new(_foglalasokLista.FoglalasokLista);
            }
            else
            {
                MessageBox.Show("Hiba történt a foglalások frissítése közben!");
            }
        }

        private void Filtereles()
        {
            Foglalasok = FilterIdo switch
            {
                0 => Foglalasok = new(_foglalasokLista.FoglalasokLista.Where(x => x.FelhasznaloNev.StartsWith(FilterSzoveg, StringComparison.InvariantCultureIgnoreCase))),
                1 => Foglalasok = new(_foglalasokLista.FoglalasokLista.Where(x => x.FelhasznaloNev.StartsWith(FilterSzoveg, StringComparison.InvariantCultureIgnoreCase) && x.Foglalasido == DateTime.Today)),
                2 => Foglalasok = new(_foglalasokLista.FoglalasokLista.Where(x => x.FelhasznaloNev.StartsWith(FilterSzoveg, StringComparison.InvariantCultureIgnoreCase) && x.Foglalasido.Month == DateTime.Today.Month)),
                _ => throw new NotImplementedException()
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
