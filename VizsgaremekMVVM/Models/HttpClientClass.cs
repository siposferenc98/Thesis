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
        private readonly string _url = "https://localhost:5001/";
        public Dictionary<string, string> Endpointok = new()
        {
            {"Burgerek" , "Burgerek"},
            {"Desszertek" , "Desszertek"},
            {"Felhasznalok" , "Felhasznalok"},
            {"Foglalasok" , "Foglalasok"},
            {"Italok" , "Italok"},
            {"Koretek" , "Koretek"},
            {"Rendelesek" , "Rendelesek"},
            {"Bevetelek" , "Rendelesek/Bevetel"},
            {"Tetelek" , "Tetelek"},
            {"PultosTetelek" , "Tetelek/Pultos"},
            {"SzakacsTetelek" , "Tetelek/Szakacs"},
            {"TetelFrissit" , "Tetelek/Status"}
        };
        public HttpClient httpClient { get; set; }
        public string Url => _url;
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
