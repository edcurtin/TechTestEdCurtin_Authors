using Common.Helpers;
using Common.Models;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechTestEdCurtin.ViewModels
{
    public class BookTabItemViewModel : ViewModelBase
    {
        #region Data Repository
        private IDataRepository bookRepository;
        #endregion

        private ObservableCollection<Book> _bookList;
        public ObservableCollection<Book> BookList
        {
            get
            {
                return _bookList;
            }
            set
            {
                _bookList = value;
                OnPropertyChanged();
            }
        }

        public BookTabItemViewModel(IDataRepository bookRepository)
        {
            BookList = new ObservableCollection<Book>();

            this.bookRepository = bookRepository;

            var x = bookRepository.GetAllBooks();


            foreach(Book b in bookRepository.GetAllBooks())
            {
                BookList.Add(b);
            }
           
            
        }

        private Book _SelectedBook;
        public Book SelectedBook
        {
            get
            {
                return _SelectedBook;
            }
            set
            {
                _SelectedBook = value;
                OnPropertyChanged();
            }
        }
     







    }
}
