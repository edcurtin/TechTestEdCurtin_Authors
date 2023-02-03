using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IDataRepository
    {
        #region Authors
        List<Author> GetAllAuthors();

      

        void AddAuthor(Author author);

        void EditAuthor(Author author);

        void DeleteAllAuthors();

        void DeleteAuthorById(long id);

        List<Author> SearchAuthor(string keys);

        #endregion


        #region Books
        List<Book> GetAllBooks();

     
        void AddBook(Book book);

        void EditBook(Book book);

        void DeleteAllBooks();

        void DeleteBookById(long id);

        List<Book> SearchBooks(string keys);

        #endregion








    }
}
