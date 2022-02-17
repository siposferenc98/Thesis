using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Linq;
using System.Windows.Media;


#nullable disable

namespace VizsgaremekMVVM.Models.BurgerEtterem
{
    public partial class Rendele : INotifyPropertyChanged
    {
        private int _etelStatus, _italStatus;
        private List<Tetel> _tetels;
        public Rendele()
        {
            Tetels = new List<Tetel>();
        }

        public int Razon { get; set; }
        public int? Fazon { get; set; }
        public int Asztal { get; set; }
        public DateTime Ido { get; set; }
        public int Etelstatus
        {
            get => _etelStatus;
            set
            {
                _etelStatus = value;
                RaisePropertyChanged("Allapot");
                RaisePropertyChanged("Brush");
            }
        }
        public int Italstatus 
        { 
            get => _italStatus;
            set
            {
                _italStatus = value;
                RaisePropertyChanged("Allapot");
                RaisePropertyChanged("Brush");
            }
        }

        public List<Tetel> Tetels
        {
            get => _tetels;
            set
            {
                _tetels = value;
                RaisePropertyChanged("Allapot");
            }
        }



        //Saját methodok,propertyk.
        public string Allapot => ToString();
        private int? Vegosszeg => Tetels is not null ? Tetels.Sum(x => x.Vegosszeg) : 0;

        [JsonIgnore]
        public Brush Brush
        {
            get
            {
                if (Etelstatus == 1 && Italstatus == 1)
                    return new SolidColorBrush(Colors.Teal);
                if (Etelstatus == 2 || Italstatus == 2)
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#0078D7");

                return new SolidColorBrush(Colors.Red);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Ez lesz meghívva minden alkalommal amikor belerakjuk egy listboxba,comboboxba, vagy csak simán kiiratjuk.
        public override string ToString()
        {
            //Alapból a statusok folyamatban vannak
            string eStatus = "Folyamatban";
            string iStatus = "Folyamatban";
            if (Etelstatus == 2) //ha 2es akkor készen van,máshova nem fog belépni, végén visszatérünk ezzel
                eStatus = "Kész!";
            if (Italstatus == 2)
                iStatus = "Kész!"; //ha 2es akkor készen van,máshova nem fog belépni, végén visszatérünk ezzel
            if (Etelstatus == 2 && Italstatus == 2) // ha mindkettő 2es akkor belépünk és visszatérünk azzal hogy felszolgálásra vár
                return $"Felszolgálásra vár!";
            if (Etelstatus == 3 && Italstatus == 3)// ha mindkettő 3as akkor belépünk és visszatérünk azzal hogy fizetésre vár + rendelés végösszegével
                return $"Fizetésre vár, összeg: {Vegosszeg} Ft.!";
            if (Etelstatus == 4 && Italstatus == 4) // ha mindkettő 4es akkor belépünk és visszatérünk azzal hogy fizetve van
                return $"Fizetve!";

            //ha sehova nem léptünk be akkor visszatérünk a statusokkal
            return $"Étel:{eStatus}, Ital: {iStatus}, Tételek: {Tetels.Count}";
        }
    }
}
