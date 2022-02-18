using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{ 
    public partial class Felhasznalo : INotifyPropertyChanged
    {
        private string _nev, _lak, _tel, _email;
        private int _jog;
        public Felhasznalo()
        {
            Foglalas = new HashSet<Foglala>();
        }

        public int Azon { get; set; }
        public string Nev 
        {
            get => _nev;
            set
            {
                _nev = value;
                RaisePropertyChanged();
            }
        }
        public string Lak
        {
            get => _lak;
            set
            {
                _lak = value;
                RaisePropertyChanged();
            }
        }
        public string Tel
        {
            get => _tel;
            set
            {
                _tel = value;
                RaisePropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        public int Jog
        {
            get => _jog;
            set
            {
                _jog = value;
                RaisePropertyChanged();
            }
        }
        public string Pw { get; set; }

        public virtual ICollection<Foglala> Foglalas { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Nev;
        }
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
