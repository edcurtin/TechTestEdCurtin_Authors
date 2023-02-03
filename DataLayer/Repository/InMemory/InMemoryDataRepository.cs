using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;


namespace DataLayer.Repository.InMemory
{
    public class InMemoryDataRepository : IDataRepository
    {

        public static List<Author> AuthorsList;
        public static List<Book> BookList;

        public InMemoryDataRepository()
        {
            AuthorsList = new List<Author>();
            BookList = new List<Book>();
        }


      

        public void AddAuthor(Author author)
        {
            if(author != null)
            {
                AuthorsList.Add(author);
            }
        }

        public void AddBook(Book book)
        {
            if (book != null)
            {
                BookList.Add(book);
            }
        }

        public void DeleteAllAuthors()
        {
            AuthorsList.Clear();
        }

        public void DeleteAllBooks()
        {
            BookList.Clear();
        }

        public void DeleteAuthorById(long id)
        {
            var findAuthor = AuthorsList.FirstOrDefault(x => x.Id == id);
            AuthorsList.Remove(findAuthor);
        }

        public void DeleteBookById(long id)
        {
            var findBook = BookList.FirstOrDefault(x => x.Id == id);
            BookList.Remove(findBook);
        }

        public void EditAuthor(Author author)
        {
            var findAuthor = AuthorsList.FirstOrDefault(x => x.Id == author.Id);
            findAuthor.firstName = author.firstName;
            findAuthor.dateOfBirth = author.dateOfBirth;
            findAuthor.secondName = author.secondName;
            
        }

        public void EditBook(Book book)
        {
            var findBook = BookList.FirstOrDefault(x => x.Id == book.Id);
            
        }

        public List<Author> GetAllAuthors()
        {
            return AuthorsList;
        }

        public List<Book> GetAllBooks()
        {
            return BookList;

        }

        public List<Author> SearchAuthor(string keys)
        {
            throw new NotImplementedException();
        }

        public List<Book> SearchBooks(string keys)
        {
            throw new NotImplementedException();
        }

    }
}
