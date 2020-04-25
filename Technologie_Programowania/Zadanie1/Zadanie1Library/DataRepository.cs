using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Zadanie1Library
{
    class DataRepository
    {
        private DataContext data;
        private DataFiller filler;
        
        public DataRepository(DataFiller filler)
        {
            this.filler = filler;
        }

        internal DataContext Data { get => data; set => data = value; }


        //obsługa książek
        public void AddKsiazka(Book book)
        {
            data.DictionaryOfBooks.Add(book.CatalogNumber, book);
        }
        
       
        public Book GetKsiazka(int index)
        {
            return data.DictionaryOfBooks[index];
        }

        public Dictionary<int, Book> GetAllKsiazki()
        {
            return data.DictionaryOfBooks;
        }

        public void UpdadeKsiazka(int index, Book book)
        {
            data.DictionaryOfBooks[index] = x;
        }

        public void DeleteKsiazka(int index)
        {
            data.DictionaryOfBooks.Remove(index);
        }

        //Obsługa Egzemnplarzy

        public void SetEgzemplarz(CopyOfBook copyOfBook)
        {
            data.ListOfCopies.Add(copyOfBook);
        }

        public CopyOfBook GetEgzemplarz(int index)
        {
            return data.ListOfCopies[index];
        }

        public List<CopyOfBook> GetAllEgzemplarz()
        {
            return data.ListOfCopies;
        }

        public void UpdadeEgzemplarz(int index, CopyOfBook copyOfBook)
        {
            data.ListOfCopies[index] = copyOfBook;
        }

        public void DeleteEgzemlarz(CopyOfBook copyOfBook)
        {
            data.ListOfCopies.Remove(copyOfBook);
        }

        //Obsługa czytelników
        public void SetCzytelnicy(Reader reader)
        {
            data.ListOfReaders.Add(reader);
        }

        public Reader GetCzytelnik(int index)
        {
            return data.ListOfReaders[index];
        }

        public List<Reader> GetAllCzytelnicy()
        {
            return data.ListOfReaders;
        }

        public void UpdadeCzytelnicy(int index, Reader reader)
        {
            data.ListOfReaders[index] = reader;
        }

        public void DeleteReader(Reader reader)
        {
            data.ListOfReaders.Remove(reader);
        }

        //Obsługa wypożyczeń
        public void SetWypozyczenia(Borrow borrow)
        {
            data.BorrowCollection.Add(borrow);
        }

        public Borrow GetWypozyczenie(int index)
        {
            return data.BorrowCollection[index];
        }

        public ObservableCollection<Borrow> GetAllWypozyczenia()
        {
            return data.BorrowCollection;
        }

        public void UpdadeWypozyczenia(int index, Borrow borrow)
        {
            data.BorrowCollection[index] = borrow;
        }

        public void DeleteWypozyczenie(Borrow borrow)
        {
            data.BorrowCollection.Remove(borrow);
        }
    }
}
