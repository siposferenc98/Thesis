using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VizsgaremekMVVM.Models
{
    public class MD5Hashing
    {
        public static string hashPW(string pw)
        {
            byte[] passtobytearr = ASCIIEncoding.ASCII.GetBytes(pw); //A beküldött jelszót átalakítjuk egy byte tömbbé.
            byte[] md5hashbytearr = new MD5CryptoServiceProvider().ComputeHash(passtobytearr); //A byte tömbből kiszámítjuk a hasht, egy adott szövegből a hash mindig ugyanaz lesz, bejelentkezésnél már hashelve küldjük be a db-be, és a 2 hasht hasonlítjuk össze.
            string md5hash = "";
            foreach (byte a in md5hashbytearr)
            {
                md5hash += a.ToString("x2"); //visszaalakítjuk hexadecimális értékeké
            }
            return md5hash;
        }
    }
}
