using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    class FillConstant : DataFiller
    {
        void DataFiller.Fill(DataContext context)
        {
            Reader cz1 = new Reader("Marek", "Szafran", "12345678989");
            Reader cz2 = new Reader("Kamil", "Łucki", "11087654321");

            context.ListOfReaders.Add(cz1);
            context.ListOfReaders.Add(cz2);

            Book k1 = new Book("Mickiewicz", "Pan Tadeusz");
            Book k2 = new Book("Prus", "Lalka");

            context.DictionaryOfBooks.Add(k1.CatalogNumber, k1);
            context.DictionaryOfBooks.Add(k2.CatalogNumber, k2);

            CopyOfBook e1 = new CopyOfBook("Ku pokrzepieniu serc.", "2018-03-17",k1);
            CopyOfBook e2 = new CopyOfBook("Powieść społeczno-obyczajowa.", "2018-03-15", k2);

            context.ListOfCopies.Add(e1);
            context.ListOfCopies.Add(e2);

            Borrow w1 = new Borrow(cz1, e1, "2018-03-20");
            Borrow w2 = new Borrow(cz2, e2, "2018-03-22");

            context.BorrowCollection.Add(w1);
            context.BorrowCollection.Add(w2);

        }
    }
}
