using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    class CopyOfBook
    {
        private string description;
        private string dateOfPurchase;
        private Book which;

        private List<Borrow> borrowList;


        public CopyOfBook(string description, string dateOfPurchase,Book which)
        {
            this.Description = description;
            this.DateOfPurchase = dateOfPurchase;
            this.which = which;
            which.ListOfCopies.Add(this);
        }

        public string Description { get => description; set => description = value; }
        public string DateOfPurchase { get => dateOfPurchase; set => dateOfPurchase = value; }
        internal Book Which { get => which; set => which = value; }
        internal List<Borrow> BorrowList { get => borrowList; set => borrowList = value; }

        public override string ToString()
        {
            return "Opis: " + this.Description + ", Data zakupu: " + this.dateOfPurchase;
        }

    }
}
