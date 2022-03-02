using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models.Lists
{
    public class Rendelesek
    {
        private HttpClientClass _http = new();
        public List<Rendele> RendelesekLista { get; set; }

        public async Task<bool> RendelesekFrissit(string? parameter = null)
        {
            var rendelesekcall = await _http.httpClient.GetAsync(_http.Url + _http.Endpointok["Rendelesek"] + parameter);
            if(rendelesekcall.IsSuccessStatusCode)
            {
                RendelesekLista = JsonSerializer.Deserialize<List<Rendele>>(await rendelesekcall.Content.ReadAsStringAsync())!;
                return true;
            }
            return false;
        }
    }
}
