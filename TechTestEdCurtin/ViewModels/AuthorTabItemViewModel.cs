using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataLayer.Interface;
using System.Collections.ObjectModel;
using Common.Models;

namespace TechTestEdCurtin.ViewModels
{
    public class AuthorTabItemViewModel : ViewModelBase
    {
        private IDataRepository bookRepository;

        public AuthorTabItemViewModel(IDataRepository bookRepository)
        {
            this.bookRepository = bookRepository;
            AuthorsList = new ObservableCollection<Author>();
            AuthorsList.Add(new Author());
        }


        private ObservableCollection<Author> _authorList;
        public ObservableCollection<Author> AuthorsList
        {
            get
            {
                return _authorList;
            }
            set
            {
                _authorList = value;
                OnPropertyChanged();
            }
        }
        
    }
}
