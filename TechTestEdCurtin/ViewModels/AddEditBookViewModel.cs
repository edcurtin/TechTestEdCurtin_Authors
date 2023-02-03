using Common.Helpers;
using Common.Models;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechTestEdCurtin.Properties;

namespace TechTestEdCurtin.ViewModels
{
    public class AddEditBookViewModel : ViewModelBase
    {
        IDataRepository bookRepo;

        private bool newBook;


        public AddEditBookViewModel(Book book, IDataRepository bookRepository)
        {
            this.bookRepo = bookRepository;
            SetUpComboDropDownData();

            if (book == null)
            {
                //this is New
                EditObject = new Book();
                EditObject.ReleaseDate = DateTime.Today;
                newBook = true;
            }
            else
            {
                //this is Edit
                EditObject = book;
                newBook = false;
            }
        }



        private void SetUpComboDropDownData()
        {
            AuthorsDict = new Dictionary<int, string>();
            var x = bookRepo.GetAllAuthors();
            foreach (Author a in x)
            {
                AuthorsDict.Add(a.Id, a.firstName + " " + a.secondName);
            }

            CategoryDict = new Dictionary<string, string>();
            CategoryDict.Add("SciFi", "SciFi");
            CategoryDict.Add("C#", "C#");
            CategoryDict.Add("OO Programming", "OO Programming");
        }

        #region Label Bindings         public string FirstName { get; set; }


        public string LblAuthorName { get { return Resource.AuthorNameHeader; } }

        public string LblTitle { get { return Resource.TitleLabel; } }

        public string LblReleaseDate { get { return Resource.ReleaseDateLabel; } }

        public string LblSynopsis { get { return Resource.SynopsisLabel; } }

        public string LblCategory { get { return Resource.CategoryLabel; } }


        #endregion



        #region ModelProperties

        private Book _editObject;
        public Book EditObject
        {
            get
            {
                return _editObject;
            }
            set
            {
                _editObject = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _CategoryKey;
        public KeyValuePair<string, string> CategoryKey
        {
            get
            {
                return _CategoryKey;
            }
            set
            {
                _CategoryKey = value;

                EditObject.Category = value.Key;

                OnPropertyChanged();
            }
        }

        private Dictionary<string, string> _categoryDict;
        public Dictionary<string, string> CategoryDict
        {
            get
            {
                return _categoryDict;
            }
            set
            {
                _categoryDict = value;
                OnPropertyChanged();
            }
        }


        private KeyValuePair<int, string> _AuthorKey;
        public KeyValuePair<int, string> AuthorKey
        {
            get
            {
                return _AuthorKey;
            }
            set
            {
                _AuthorKey = value;

                if(EditObject != null)
                {
                    EditObject.AuthorId = value.Key;
                }

                OnPropertyChanged();
            }
        }

        private Dictionary<int, string> _authorsDict;
        public Dictionary<int, string> AuthorsDict
        {
            get
            {
                return _authorsDict;
            }
            set
            {
                _authorsDict = value;
                OnPropertyChanged();
            }
        }



        #endregion


        #region Button Commands
        private ICommand _applyCommmand;
        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommmand == null)
                {
                    _applyCommmand = new RelayCommand(param => this.ApplyPressed());
                }
                return _applyCommmand;
            }
        }

        private void ApplyPressed()
        {
            if (EditObject != null)
            {
                if (newBook)
                {
                    var AllBooks = bookRepo.GetAllBooks();
                    if (AllBooks != null && AllBooks.Count > 0)
                    {
                        int intIdt = AllBooks.Max(u => u.Id);
                        EditObject.Id = intIdt + 1;
                    }
                    else
                    {
                        EditObject.Id = 1;
                    }
                    bookRepo.AddBook(EditObject);
                }
                else
                {
                    bookRepo.EditBook(EditObject);
                }

                
                closeEditor("BOOK");
            }
        }

        private ICommand _cancelCommand;
        internal DoFunkyStuff closeEditor;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(param => this.CancelPress());
                }
                return _cancelCommand;
            }
        }

        private void CancelPress()
        {
            EditObject = null;
            closeEditor("BOOK");
        }
        #endregion


    }
}
