using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using VizsgaremekMVVM.Models.Lists;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using VizsgaremekMVVM.Models.Buttons;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class TermekekVM : INotifyPropertyChanged
    {
        public Termekek Termekek { get; set; } = new();
        public ICommand ListBoxItemClick => new Button(ListboxClick);

        public TermekekVM()
        {
            TermekekFrissit();
        }
        private async void TermekekFrissit()
        {
            if (await Termekek.ListakFrissit())
            {
                RaisePropertyChanged("Termekek");
            }
        }

        public void ListboxClick(object? o)
        {
            ITermek termek = (ITermek)o!;
            Views.Admin.TermekModositas termekmodositas = new(termek);
            if (termekmodositas.ShowDialog() == true)
            {
                TermekekFrissit();
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
