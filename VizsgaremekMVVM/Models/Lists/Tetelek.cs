using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VizsgaremekMVVM.Models.BurgerEtterem;

namespace VizsgaremekMVVM.Models.Lists
{
    public class Tetelek
    {
        private HttpClientClass _http = new();
        public List<Tetel> TetelLista { get; set; }

        public async Task<bool> TetelekFrissit(string? parameter = null)
        {
            var tetelcall = await _http.httpClient.GetAsync(_http.url + "Tetelek" + parameter);
            if (tetelcall.IsSuccessStatusCode)
            {
                TetelLista = JsonSerializer.Deserialize<List<Tetel>>(await tetelcall.Content.ReadAsStringAsync())!;
                return true;
            }
            return false;
        }
    }
}
