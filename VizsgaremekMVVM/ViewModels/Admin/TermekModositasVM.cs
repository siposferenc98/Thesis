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

namespace VizsgaremekMVVM.ViewModels.Admin
{
    class TermekModositasVM : INotifyPropertyChanged
    {

        public event EventHandler TermekModositva;
        private int _kivalasztottTermek = 0;
        public int KivalasztottTermek 
        {
            get => _kivalasztottTermek;
            set
            {
                _kivalasztottTermek = value;
                RaisePropertyChanged();
            }
        }
        public ICommand TermekFrissitButton => new ButtonCE(TermekFrissit,()=>true);

        public TermekModositasVM(ITermek termek)
        {
            KivalasztottTermek = termek.TermekFajta;
        }

        private void TermekFrissit(object? o)
        {
            TermekModositva.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
