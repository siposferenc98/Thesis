using System;
using System.Windows;
using VizsgaremekMVVM.Models.BurgerEtterem;
using VizsgaremekMVVM.ViewModels.Admin;

namespace VizsgaremekMVVM.Views.Admin
{
    /// <summary>
    /// Interaction logic for TermekModositas.xaml
    /// </summary>
    public partial class TermekModositas : Window
    {
        public TermekModositas(ITermek termek)
        {
            InitializeComponent();
            TermekModositasVM termekModositasVM = new(termek);
            DataContext = termekModositasVM;
            termekModositasVM.TermekModositva += TermekModositasVM_TermekModositva;
        }

        private void TermekModositasVM_TermekModositva(object? sender, EventArgs e)
        {
            DialogResult = true;
        }
    }
}
