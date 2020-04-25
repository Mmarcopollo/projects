using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    class Borrow
    {
        private Reader who;
        private CopyOfBook which;
        private string dateOfBorrow;
        private string dateOfReturn;

        public Borrow(Reader who, CopyOfBook which, string dateOfBorrow)
        {
            this.Who = who;
            this.Which = which;
            this.DateOfBorrow = dateOfBorrow;
            DateOfReturn = null;
            who.ListOdBorrow.Add(this);
            which.BorrowList.Add(this);
        }

        public Reader Who { get => who; set => who = value; }
        public string DateOfBorrow { get => dateOfBorrow; set => dateOfBorrow = value; }
        public string DateOfReturn { get => dateOfReturn; set => dateOfReturn = value; }
        internal CopyOfBook Which { get => which; set => which = value; }

        public override string ToString()
        {
            return "Kto: " + this.who + ", Co: " + this.which + ", Data wypozyczenia: " + dateOfBorrow +", Data oddania: "+this.DateOfReturn;
        }
    }
}
