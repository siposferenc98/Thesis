using System.Windows;
using VizsgaremekMVVM.ViewModels.Felszolgalo;
using VizsgaremekMVVM.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using VizsgaremekMVVM.Models.Lists;
using System.Linq;
using System.Windows.Media;


namespace VizsgaremekMVVM.Views.Felszolgalo
{
    /// <summary>
    /// Interaction logic for FelszolgaloUI.xaml
    /// </summary>
    public partial class FelszolgaloUI : Window
    {
        public FelszolgaloUI()
        {
            InitializeComponent();
            FelszolgaloUIVM felszolgaloUIVM = new();
            DataContext = felszolgaloUIVM;
            felszolgaloUIVM.AsztalokRajzol += asztalokSzinez;
            kijelentkezesMenu.Click += Kijelentkezes.kijelentkezes;
            jelszoValtoztatMenu.Click += JelszoValtoztatEljaras.jelszoValtoztat;
        }
       
        private void asztalokSzinez(object? sender, EventArgs e)
        {
            AsztalokSzinezEventArgs args = (AsztalokSzinezEventArgs)e;
            UIElementCollection asztalok = AsztalokGrid.Children;
            foreach (object asztal in asztalok)
            {
                Button a = (Button)asztal;
                if (args.Rendelesek.Any(x => x.Asztal == int.Parse((string)a.Tag)))
                    a.Background = new SolidColorBrush(Colors.Teal);
                else
                    a.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
    }
}
