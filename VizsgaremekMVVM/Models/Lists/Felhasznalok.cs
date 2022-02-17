using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models.Lists
{
    public class Felhasznalok
    {
        private HttpClientClass _http = new();
        public List<Felhasznalo> FelhasznaloLista { get; set; }

        public async Task<bool> FelhasznalokFrissit(string? parameter = null)
        {
            var felhasznalokcall = await _http.httpClient.GetAsync(_http.url + "Felhasznalok" + parameter);
            if(felhasznalokcall.IsSuccessStatusCode)
            {
                FelhasznaloLista = JsonSerializer.Deserialize<List<Felhasznalo>>(await felhasznalokcall.Content.ReadAsStringAsync())!;
                return true;
            }
            return false;
        }
    }
}
