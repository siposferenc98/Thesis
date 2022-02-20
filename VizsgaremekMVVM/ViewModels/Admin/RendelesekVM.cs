using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.Models.Lists;
using VizsgaremekMVVM.Models;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class RendelesekVM : INotifyPropertyChanged
    {
        private Rendelesek _rendelesekLista = new();
        private ObservableCollection<Grid> _rendelesek = new();
        public ObservableCollection<Grid> Rendelesek
        {
            get => _rendelesek;
            set
            {
                _rendelesek = value;
                RaisePropertyChanged();
            }
        }
        public RendelesekVM()
        {
            RendelsekListaFrissit();
        }

        private async void RendelsekListaFrissit()
        {
            if (await _rendelesekLista.RendelesekFrissit())
            {
                RendelesStackPanelGen();
            }
            else
            {
                MessageBox.Show("Hiba történt a rendelések frissítése közben!");
            }
        }

        private void RendelesStackPanelGen()
        {
            _rendelesekLista.RendelesekLista.ForEach(x =>
            {
                Rendelesek.Add(RendelesStackPanelExpander.rendelesElemKeszit(x));
            });
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
