using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    public class Reader
    {
        private string name;
        private string surname;
        private string PESEL;

        private List<Borrow> listOdBorrow;

        public Reader(string name, string surname, string PESEL)
        {
            this.Name = name;
            this.Surname = surname;
            this.PESEL1 = PESEL;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string PESEL1 { get => PESEL; set => PESEL = value; }
        internal List<Borrow> ListOdBorrow { get => listOdBorrow; set => listOdBorrow = value; }

        public override string ToString()
        {
            return "Imie: " + this.name + ", Nazwisko: " + this.surname + ", PESEL: " + PESEL;
        }
    }
}
