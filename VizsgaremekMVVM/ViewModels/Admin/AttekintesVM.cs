using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        private Rendelesek _rendelesek = new();
        public BindingList<Rendele> AktivRendelesek { get; set; } = new();

        public event EventHandler AsztalokRajzol;
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
                    if (await _rendelesek.RendelesekFrissit("/Aktiv") && Application.Current is not null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AktivRendelesek = new(_rendelesek.RendelesekLista);
                            AsztalokRajzol?.Invoke(this, new AsztalokSzinezEventArgs() { Rendelesek = AktivRendelesek });
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
    }
}
