using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Zadanie1Library
{
    class DataService
    {
        private DataRepository repository;

        public DataService(DataRepository repository)
        {
            this.repository = repository;
        }

        public void ShowReaders(List<Reader> list)
        {
          for( int i=0; i < list.Count; i++)
           {
                Console.WriteLine(list[i]);
           }
        }

        public void ShowBooksCopies(List<CopyOfBook> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
             }
        }

        public void ShowBooks(Dictionary<int,Book> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        public void ShowBorrows(ObservableCollection<Borrow> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        public void ShowReaderBorrows(Reader x)
        {
            for (int i = 0; i < repository.Data.BorrowCollection.Count; i++)
            {
                if(repository.Data.BorrowCollection[i].Who.PESEL1 == x.PESEL1)
                {
                    Console.WriteLine(repository.Data.BorrowCollection[i]);
                }
              
            }
        }

        public void ShowBorrowInfo(Borrow x)
        {
            for (int i = 0; i < repository.Data.BorrowCollection.Count; i++)
            {
                if (repository.Data.BorrowCollection[i].Who == x.Who && repository.Data.BorrowCollection[i].Which ==x.Which)
                {
                    Console.WriteLine(repository.Data.BorrowCollection[i]);
                }

            }
        }

        public void ShowBookInfo(Book x)
        {
            for (int i = 0; i < repository.Data.BorrowCollection.Count; i++)
            {
                if (repository.Data.BorrowCollection[i].Which.Which.CatalogNumber == x.CatalogNumber)
                {
                    Console.WriteLine(repository.Data.BorrowCollection[i]);
                }
            }
        }

        public void ChangeDescriptionOfBook(CopyOfBook x, string description)
        {
            for(int i = 0; i < repository.Data.ListOfCopies.Count;i++)
            {
                if(repository.Data.ListOfCopies[i] == x)
                {
                    repository.Data.ListOfCopies[i].Description = description;
                }
            }
        }

        public void MakeBorrow(CopyOfBook what, Reader who, string DateOfBorrow)
        {
            Borrow x = new Borrow(who, what, DateOfBorrow);
           repository.Data.BorrowCollection.Add(x);
        }

        public void BookReturn(Borrow x, string date)
        {
            for(int i = 0; i < repository.Data.BorrowCollection.Count; i++)
            {
                if(repository.Data.BorrowCollection[i] == x)
                {
                    repository.Data.BorrowCollection[i].DateOfReturn = date;
                }
            }
        }

   
    }
}
