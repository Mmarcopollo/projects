using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Zadanie1Library
{
    class DataContext
    {
        private List<Reader> listOfReaders;
        private List<CopyOfBook> listOfCopies;
        private Dictionary<int, Book> dictionaryOfBooks;
        private ObservableCollection<Borrow> borrowCollection;

        public List<Reader> ListOfReaders { get => listOfReaders; set => listOfReaders = value; }
        internal List<CopyOfBook> ListOfCopies { get => listOfCopies; set => listOfCopies = value; }
        internal Dictionary<int, Book> DictionaryOfBooks { get => dictionaryOfBooks; set => dictionaryOfBooks = value; }
        internal ObservableCollection<Borrow> BorrowCollection { get => borrowCollection; set => borrowCollection = value; }
    }
}
