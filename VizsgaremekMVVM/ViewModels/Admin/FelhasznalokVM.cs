using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VizsgaremekMVVM.Models.Lists;
using VizsgaremekMVVM.Models.BurgerEtterem;
using System.Windows.Input;
using VizsgaremekMVVM.Models.Buttons;
using System;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class FelhasznalokVM : INotifyPropertyChanged
    {
        private Felhasznalok _felhasznalok = new();
        private ObservableCollection<Felhasznalo> _alkalmazottak;
        private ObservableCollection<Felhasznalo> _vendegek;
        public ObservableCollection<Felhasznalo> Alkalmazottak 
        {
            get => _alkalmazottak;
            set
            {
                _alkalmazottak = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Felhasznalo> Vendegek
        {
            get => _vendegek;
            set
            {
                _vendegek = value;
                RaisePropertyChanged();
            }
        }
        public ICommand FelhasznaloMVAdminButton => new Button(FelhasznaloModositasaVagyAdminFHozzaadasa);

        public FelhasznalokVM()
        {
            FelhasznalokFrissitese();
        }
        private async void FelhasznalokFrissitese()
        {
            if (await _felhasznalok.FelhasznalokFrissit())
            {
                Alkalmazottak = new(_felhasznalok.FelhasznaloLista.Where(x => x.Jog > 0));
                Vendegek = new(_felhasznalok.FelhasznaloLista.Where(x => x.Jog == 0));
            }
        }
        private void FelhasznaloModositasaVagyAdminFHozzaadasa(object? o)
        {
            Views.Regisztracio regisztracio;
            if (o is not null)
            {
                Felhasznalo f = (Felhasznalo)o;
                regisztracio = new(f);
            }
            else
            {
                regisztracio = new(adminRegisztracio: true);
            }

            if (regisztracio.ShowDialog() == true)
            {
                FelhasznalokFrissitese();
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
