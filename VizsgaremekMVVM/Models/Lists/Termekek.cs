using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models.Lists
{
    public class Termekek
    {
        private HttpClientClass _http = new();
        public ObservableCollection<Burger> Burgerek { get; set; }
        public ObservableCollection<Koret> Koretek { get; set; }
        public ObservableCollection<Ital> Italok { get; set; }
        public ObservableCollection<Desszert> Desszertek { get; set; }

        public Termekek()
        {
            
        }
         
        public async Task<bool> ListakFrissit(string? parameter = null)
        {
            var hamburgerCall = await _http.httpClient.GetAsync(_http.url + "Burgerek" +parameter);
            var koretekCall = await _http.httpClient.GetAsync(_http.url + "Koretek" + parameter);
            var italokCall = await _http.httpClient.GetAsync(_http.url + "Italok" + parameter);
            var desszertekCall = await _http.httpClient.GetAsync(_http.url + "Desszertek" + parameter);
            if (hamburgerCall.IsSuccessStatusCode && koretekCall.IsSuccessStatusCode && italokCall.IsSuccessStatusCode && desszertekCall.IsSuccessStatusCode)
            {
                Burgerek = JsonSerializer.Deserialize<ObservableCollection<Burger>>(await hamburgerCall.Content.ReadAsStringAsync())!;
                Koretek = JsonSerializer.Deserialize<ObservableCollection<Koret>>(await koretekCall.Content.ReadAsStringAsync())!;
                Italok = JsonSerializer.Deserialize<ObservableCollection<Ital>>(await italokCall.Content.ReadAsStringAsync())!;
                Desszertek = JsonSerializer.Deserialize<ObservableCollection<Desszert>>(await desszertekCall.Content.ReadAsStringAsync())!;
                return true;
            }
            else
            {
                MessageBox.Show("Hiba történt a termékek lekérése közben!");
                return false;
            }
        }

    }
}
