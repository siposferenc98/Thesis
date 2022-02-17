using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VizsgaremekMVVM.Models
{
    public class HttpClientClass
    {
        public string url = "https://localhost:5001/";
        public HttpClient httpClient { get; set; }
        public HttpClientClass()
        {
            httpClient = new();
            httpClient.DefaultRequestHeaders.Add("Auth", AktivFelhasznalo.Token);
        }
        public StringContent contentKrealas<T>(T obj)
        {
            StringContent content = new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
            return content;
        }
    }
}
