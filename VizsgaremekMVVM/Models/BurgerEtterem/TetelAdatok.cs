using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    internal class TetelAdatok : INotifyPropertyChanged
    {
        private Burger _burger;
        private Koret _koret;
        private Ital _ital;
        private Desszert _desszert;
        private string _megjegyzes;
        private int? _bdb = 1, _kdb = 1, _idb = 1, _ddb = 1;
        public Burger Burger
        {
            get => _burger;
            set
            {
                _burger = value;
                RaisePropertyChanged();
            }
        }
        public Koret Koret
        {
            get => _koret;
            set
            {
                _koret = value;
                RaisePropertyChanged();
            }
        }
        public Ital Ital
        {
            get => _ital;
            set
            {
                _ital = value;
                RaisePropertyChanged();
            }
        }
        public Desszert Desszert
        {
            get => _desszert;
            set
            {
                _desszert = value;
                RaisePropertyChanged();
            }
        }
        public int? Bdb
        {
            get => _bdb;
            set
            {
                _bdb = value;
                RaisePropertyChanged();
            }
        }
        public int? Kdb
        {
            get => _kdb;
            set
            {
                _kdb = value;
                RaisePropertyChanged();
            }
        }
        public int? Idb
        {
            get => _idb;
            set
            {
                _idb = value;
                RaisePropertyChanged();
            }
        }
        public int? Ddb
        {
            get => _ddb;
            set
            {
                _ddb = value;
                RaisePropertyChanged();
            }
        }
        public string Megjegyzes
        {
            get => _megjegyzes;
            set
            {
                _megjegyzes = value;
                RaisePropertyChanged();
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
