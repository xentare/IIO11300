using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtävä9
{
    public class Pelaaja
    {
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public double Siirtohinta { get; set; }
        public string Seura { get; set; }
        public string KokoNimi
        {
            get { return Etunimi + " " + Sukunimi + ", " + Seura + " " + Siirtohinta; }
        }

        public Pelaaja(string Etunimi, string Sukunimi, double Siirtohinta, string Seura)
        {
            this.Etunimi = Etunimi;
            this.Seura = Seura;
            this.Sukunimi = Sukunimi;
            this.Siirtohinta = Siirtohinta;
        }

        override
            public string ToString()
        {
            return KokoNimi;
        }
    }
}
