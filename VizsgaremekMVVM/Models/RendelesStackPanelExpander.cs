using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models
{
    public class RendelesStackPanelExpander
    {
        private static Dictionary<string, string> bindingNevErtek = new()
        {
            //{ "tazon", "Tétel azonosító"},
            { "BazonNavigation", "Hamburger"},
            { "Bdb", "DB"},
            { "DazonNavigation", "Desszert"},
            { "Ddb", "DB"},
            { "KazonNavigation", "Köret"},
            { "Kdb", "DB" },
            { "IazonNavigation", "Ital" },
            { "Idb", "DB"},
            { "Vegosszeg", "Végösszeg"}
        };
        
        /// <summary>
        /// Egy lenyitható stackpanel elemet készít a rendelésből, aminek a tartalma egy DataGrid a tételekkel.
        /// </summary>
        /// <param name="r">Egy rendelés</param>
        /// <returns>Stackpanel elem, benne a tételeivel</returns>
        public static Grid rendelesElemKeszit(Rendele r)
        {
            
            Grid g = new();

            Expander ex = new(); //expander nem tartalmazhat több elemet, csak .Content-je lehet, szóval a contentje a datagridünk lesz
            ex.Header = $"{r.Razon}. számú rendelés, {r.Ido:yyyy.M.d HH.mm}";
            ex.Tag = r;
            ex.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            ex.Expanded += Ex_Expanded;
            g.Children.Add(ex);
            return g;

        }

        private static void Ex_Expanded(object sender, RoutedEventArgs e)
        {
            Expander ex = (Expander)sender;
            ex.Content = expanderKattint((Rendele)ex.Tag);
        }

        public static DataGrid expanderKattint(Rendele r)
        {
            ObservableCollection<Tetel> ts = new(r.Tetels); //létrehozunk egy ObservableCollectiont a tételek listából,ezt fogjuk használni a gridünk tartalmához.

            DataGrid dataGrid = new(); //létrehozunk egy datagridet
            dataGrid.Margin = new Thickness(10);
            dataGrid.AutoGenerateColumns = false; //ne generálja ki az összes oszlopot nekünk előlre névvel mert majd mi hozzáadjuk amik kellenek
            dataGrid.IsReadOnly = true; //ne tudják a gridet szerkeszteni
            dataGrid.ItemsSource = ts; //ami az új observablecollection

            foreach (KeyValuePair<string, string> keyValuePair in bindingNevErtek) //végigmegyünk párossával a dictionaryn
            {
                //minden elemhez készítünk egy oszlopot
                DataGridTextColumn dataGridColumn = new();
                dataGridColumn.Header = keyValuePair.Value; //aminek legfelül a headerje a dict. értéke lesz, amit látni szeretnék a UI-on
                dataGridColumn.Binding = new Binding(keyValuePair.Key); //a binding a dict. key lesz,ez létrehoz egy databindingot
                //mivel a datagridünk itemssource-a az observablecollection ami tételeket tartalmaz, a binding útvonala a tétel classon belül fog keresni ilyen nevű propertyt, amihez hozzá tudja kötni az értéket
                dataGrid.Columns.Add(dataGridColumn); //végül hozzáadjuk a datagridünkhöz ezt az oszlopot
            }

            return dataGrid;
        }
    }
}
