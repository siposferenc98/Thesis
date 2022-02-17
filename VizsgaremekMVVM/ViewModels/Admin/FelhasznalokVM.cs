using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VizsgaremekMVVM.Models.Lists;
using VizsgaremekMVVM.Models.BurgerEtterem;

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
        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
