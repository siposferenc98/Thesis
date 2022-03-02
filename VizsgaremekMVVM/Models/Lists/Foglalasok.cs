using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models.Lists
{
    public class Foglalasok
    {
        private HttpClientClass _http = new();
        public List<Foglala> FoglalasokLista { get; set; }

        public async Task<bool> FoglalasokFrissit(string? parameter = null)
        {
            var foglalasokcall = await _http.httpClient.GetAsync(_http.Url + _http.Endpointok["Foglalasok"] + parameter);
            if(foglalasokcall.IsSuccessStatusCode)
            {
                FoglalasokLista = JsonSerializer.Deserialize<List<Foglala>>(await foglalasokcall.Content.ReadAsStringAsync())!;
                return true;
            }
            return false;
        }
    }
}
