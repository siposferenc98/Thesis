using NUnit.Framework;
using System.Collections.ObjectModel;
using VizsgaremekMVVM.Models;
using VizsgaremekMVVM.Models.Buttons;

namespace VizsgaremekTest
{
    internal record HttpTestRecord(int Id, string Nev, int[] szamok);

    public class Tests
    {
        private int ertek;
        private bool ceFeltetel;
        [SetUp]
        public void Setup()
        {
            ertek = 0;
            ceFeltetel = false;
        }

        [Test]
        public void MD5HashAlgoritmusTest()
        {
            string hashedmd5pw = MD5Hashing.hashPW("unittest");
            Assert.AreEqual("16802231b09f155b7a42a5dcaba33a74", hashedmd5pw);
        }
        [Test]
        public void HttpClientTest()
        {
            HttpTestRecord test = new(1, "testing", new int[] { 1, 2, 3 });
            HttpClientClass _http = new();
            var stringContent = _http.contentKrealas(test);
            string content = stringContent.ReadAsStringAsync().Result;
            Assert.AreEqual("{\"Id\":1,\"Nev\":\"testing\",\"szamok\":[1,2,3]}", content);
        }
        [Test]
        public void ButtonTest()
        {

            Button button = new((o) => ertek = 1);
            button.Execute(null);
            Assert.AreEqual(1, ertek);
        }
        [Test]
        public void ButtonCanExecuteTest()
        {
            ButtonCE button = new((o) => ertek = 1, () => ceFeltetel);
            Assert.IsFalse(button.CanExecute(null));
            ceFeltetel = true;
            Assert.IsTrue(button.CanExecute(null));
        }
        [Test]
        public void ObservableCollectionExtensionMethodTest()
        {
            ObservableCollection<int> collection = new() { 128, 2, 338, 1000, 500 };
            collection.Sort();
            Assert.AreEqual(new ObservableCollection<int> { 2, 128, 338, 500, 1000 }, collection);
        }
    }
}