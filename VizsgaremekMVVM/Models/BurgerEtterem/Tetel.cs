using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Tetel : INotifyPropertyChanged
    {
        private Brush _brush;
        private Stopwatch _stopwatch = new();
        private int? _idb;
        private Ital _ital;
        private string _megjegyzes;

        public Tetel()
        {
            StopwatchFigyelSzinValtoztat();
        }

        public int Tazon { get; set; }
        public int? Razon { get; set; }
        public int Bazon { get; set; }
        public int? Bdb { get; set; }
        public int Kazon { get; set; }
        public int? Kdb { get; set; }
        public int Dazon { get; set; }
        public int? Ddb { get; set; }
        public int Iazon{ get; set; }
        public int? Idb 
        {
            get => _idb;
            set
            {
                _idb = value;
                RaisePropertyChanged();
            }
        }
        public int Etelstatus { get; set; }
        public int Italstatus { get; set; }
        public string Megjegyzes 
        {
            get => _megjegyzes;
            set
            {
                _megjegyzes = value;
                RaisePropertyChanged();
            }
        }

        public virtual Burger BazonNavigation { get; set; }
        public virtual Desszert DazonNavigation { get; set; }
        public virtual Ital IazonNavigation
        {
            get => _ital;
            set
            {
                _ital = value;
                RaisePropertyChanged();
            }
        }
        public virtual Koret KazonNavigation { get; set; }



        //Saját methodok,propertyk.
        
        public string Status => GetStatus();
        [JsonIgnore]
        public string SzakacsReszletek => GetSzakacsReszletek();
        [JsonIgnore]
        public string PultosReszletek => GetPultosReszletek();

        [JsonIgnore]
        public int? Vegosszeg => BazonNavigation.Bar * Bdb + KazonNavigation.Kar * Kdb + DazonNavigation.Dar * Ddb + IazonNavigation.Iar * Idb;
        [JsonIgnore]
        public Brush Brush
        {
            get => _brush;
            set
            {
                if (_brush != value)
                {
                    _brush = value;
                    RaisePropertyChanged();
                }
            }
        }

        private async void StopwatchFigyelSzinValtoztat()
        {
            _stopwatch.Start();
            while (_stopwatch.IsRunning)
            {
                if (Application.Current is not null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (_stopwatch.Elapsed > TimeSpan.FromSeconds(60))
                        {
                            Brush = new SolidColorBrush(Colors.Red);
                            _stopwatch.Stop();
                        }
                        else if(_stopwatch.Elapsed > TimeSpan.FromSeconds(30))
                        {
                            Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#0078D7");
                        }
                        else
                        {
                            Brush = new SolidColorBrush(Colors.Teal);
                        }
                    });
                }

                await Task.Delay(1000);
            }
        }
        private string GetSzakacsReszletek()
        {
            string ki = "";
            if (BazonNavigation.Bazon > 1)
                ki += $"{Bdb} {BazonNavigation.Bnev}, ";
            if (KazonNavigation.Kazon > 1)
                ki += $"{Kdb} {KazonNavigation.Knev}, ";
            if (DazonNavigation.Dazon > 1)
                ki += $"{Ddb} {DazonNavigation.Dnev}, ";
            ki += $"{Razon}. számú rendeléshez.";
            return ki;
        }
        private string GetPultosReszletek()
        {
            return $"{Idb} {IazonNavigation.Inev} {Razon}. számú rendeléshez.";
        }

        private string GetStatus()
        {
            string etelstatus = "Folyamatban";
            string italstatus = "Folyamatban";
            if (Etelstatus == 2)
                etelstatus = "Kész!";
            if (Italstatus == 2)
                italstatus = "Kész!";
            return $"Étel: {etelstatus}, Ital: {italstatus}";
        }
        public void SetErtekek(Tetel t)
        {
            Bazon = t.Bazon;
            Bdb = t.Bdb;
            Kazon = t.Kazon;
            Kdb = t.Kdb;
            Dazon = t.Dazon;
            Ddb = t.Ddb;
            Iazon = t.Iazon;
            Idb = t.Idb;
            Megjegyzes = t.Megjegyzes;
            BazonNavigation = t.BazonNavigation;
            KazonNavigation = t.KazonNavigation;
            DazonNavigation = t.DazonNavigation;
            IazonNavigation = t.IazonNavigation;
            RaisePropertyChanged("Reszletek");
            RaisePropertyChanged("Status");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
