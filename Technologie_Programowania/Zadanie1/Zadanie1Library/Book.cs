using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    class Book
    {
        private string autor;
        private string title;
        private int catalogNumber;

        private List<CopyOfBook> listOfCopies = new List<CopyOfBook>();

        public Book(string autor, string title) 
        {
            this.Autor = autor;
            this.Title = title;
            CatalogNumber = BookKeyGenerator.generateKey();
        }

        public string Autor { get => autor; set => autor = value; }
        public string Title { get => title; set => title = value; }
        public int CatalogNumber { get => catalogNumber; set => catalogNumber = value; }
        internal List<CopyOfBook> ListOfCopies { get => listOfCopies; set => listOfCopies = value; }

        public override string ToString()
        {
           return "Autor: " +this.autor + ", Tytuł: " + this.title + ", Numet katakolgowy: " + CatalogNumber;
        }
    }
}
