using Common.Helpers;
using Common.Models;
using DataLayer.Interface;
using DataLayer.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

delegate void DoFunkyStuff(string info);

namespace TechTestEdCurtin.ViewModels
{


    public class WrapperViewModel : ViewModelBase
    {
      

        #region Data Repository
        private IDataRepository bookRepository;
        #endregion

        #region View Model Properties

        #endregion

        #region Database Interaction

        #endregion

        private AddEditAuthorViewModel _AddEditAuthorViewModel;
        public AddEditAuthorViewModel AddEditAuthorViewModel
        {
            get
            {
                return _AddEditAuthorViewModel;
            }
            set
            {
                _AddEditAuthorViewModel = value;
                OnPropertyChanged();
            }
        }


      

        private ICommand _AddBookCommand;
        public ICommand AddBookCommand
        {
            get
            {
                if(_AddBookCommand == null)
                {
                    _AddBookCommand = new RelayCommand(param => AddBook());
                }
                return _AddBookCommand;
            }
           
        }

        private void AddBook()
        {
            AddEditBook();
        }


        private ICommand _DeleteBookCommand;
        public ICommand DeleteBookCommand
        {
            get
            {
                if (_DeleteBookCommand == null)
                {
                    _DeleteBookCommand = new RelayCommand(param => DeleteBook((Book)param));
                }
                return _DeleteBookCommand;
            }
        }

        private void DeleteBook(Book selectedBook)
        {
            bookRepository.DeleteBookById(selectedBook.Id);
            InstantiateBooksTabViewModel();
        }

        private void AddEditBook(Book book = null)
        {
            DoFunkyStuff closeEditorDel = new DoFunkyStuff(CloseEditorWindowAndReloadGrid);
            if (book == null)
            {
                AddEditBookViewModel = new AddEditBookViewModel(null, bookRepository);
            }
            else
            {
                AddEditBookViewModel = new AddEditBookViewModel(book, bookRepository);
            }
            AddEditBookViewModel.closeEditor = closeEditorDel;
            AddEditBookVisibility = Visibility.Visible;
        }

        private ICommand _EditBookCommand;
        public ICommand EditBookCommand
        {
            get
            {
                if (_EditBookCommand == null)
                {
                    _EditBookCommand = new RelayCommand(param => EditBook((Book)param));
                }
                return _EditBookCommand;
            }
        }

        private void EditBook(Book selectedBook)
        {
            AddEditBook(selectedBook);

        }

        private Visibility _AddEditBookVisibility;
        public Visibility AddEditBookVisibility
        {
            get
            {
                return _AddEditBookVisibility;
            }
            set
            {
                _AddEditBookVisibility = value;
                OnPropertyChanged();
            }
        }

        private AddEditBookViewModel _AddEditBookViewModel;
        public AddEditBookViewModel AddEditBookViewModel
        {
            get
            {
                return _AddEditBookViewModel;
            }
            set
            {
                _AddEditBookViewModel = value;
                OnPropertyChanged();
            }
        }



        private ICommand _GetAuthorFullName;
        public ICommand GetAuthorFullName
        {
            get
            {
                if(_GetAuthorFullName == null)
                {
                    _GetAuthorFullName = new RelayCommand(param => this.GetAuthorName(param));
                }
                return _GetAuthorFullName;
            }
        }

        private void GetAuthorName(object param)
        {
            TextBlock tBlk = (TextBlock)param;
            Debug.WriteLine(tBlk.Tag);
            if(tBlk.Tag != null)
            {
                var Author = AuthorsList.FirstOrDefault(x => x.Id.ToString() == tBlk.Tag.ToString());
                tBlk.Text = Author.firstName + " " + Author.secondName;

            }
         


        }

        private ICommand _TabItemLoaded;
        public ICommand TabItemLoaded
        {
            get
            {
                if (_TabItemLoaded == null)
                {
                    _TabItemLoaded = new RelayCommand(param => CreateTabItemViewModel((TabItem)param));
                }
                return _TabItemLoaded;
            }
        }

        private void CreateTabItemViewModel(TabItem tabItem)
        {
            switch (tabItem.Name)
            {
                case "BooksTabItem":
                    InstantiateBooksTabViewModel();
                    break;
                case "AuthorsTabItem":
                    InstantiateAuthorsTabViewModel();
                    break;
            }



        }

        private void InstantiateAuthorsTabViewModel()
        {
            AuthorTabItemViewModel = new AuthorTabItemViewModel(bookRepository);
            CloseEditorScreens();
        }

        private void InstantiateBooksTabViewModel()
        {
            BookTabItemViewModel = new BookTabItemViewModel(bookRepository);
            CloseEditorScreens();
        }


        private void CloseEditorScreens()
        {
            AddEditBookVisibility = Visibility.Collapsed;
            
        }

        private BookTabItemViewModel _bookTabItemViewModel;
        public BookTabItemViewModel BookTabItemViewModel
        {
            get
            {
                return _bookTabItemViewModel;
            }
            set
            {
                _bookTabItemViewModel = value;
                OnPropertyChanged();
            }
        }

        private AuthorTabItemViewModel _authorTabItemViewModel;
        public AuthorTabItemViewModel AuthorTabItemViewModel
        {
            get
            {
                return _authorTabItemViewModel;
            }
            set
            {
                _authorTabItemViewModel = value;
                OnPropertyChanged();
            }
        }


        private List<Author> AuthorsList;

        public WrapperViewModel()
        {
            CloseEditorScreens();

            this.bookRepository = new InMemoryDataRepository();

            //set up author - dont have time to allow edit / delete so hardcoded for now but persistant during runtime
            Author a1 = new Author();
            a1.Id = 1;
            a1.firstName = "Tim";
            a1.secondName = "Adams";
            a1.dateOfBirth = Convert.ToDateTime("01/08/2008 14:50:50.42");

            bookRepository.AddAuthor(a1);

            Author a2 = new Author();
            a2.Id = 2;
            a2.firstName = "Ed";  
            a2.secondName = "Curtin";
            a2.dateOfBirth = Convert.ToDateTime("01/08/1977 14:50:50.42");

            bookRepository.AddAuthor(a2);

            AuthorsList = bookRepository.GetAllAuthors();



        }

        private void CloseEditorWindowAndReloadGrid(string info)
        {
            switch (info)
            {
                case "BOOK"://reload Book Grid Close Editor
                    InstantiateBooksTabViewModel();
                    break;
                case "AUTHOR"://reload Author Grid Close Editor
                    InstantiateAuthorsTabViewModel();
                    break;
            }
            
        }

     
    }
}
