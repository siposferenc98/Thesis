using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models;

namespace VizsgaremekMVVM.ViewModels.Admin
{
    internal class AdminUIVM : INotifyPropertyChanged
    {
        public AdminUIVM()
        {
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
