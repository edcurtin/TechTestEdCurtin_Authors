using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    //Author, Title, Release Date, Synopsis, Category
    public class Book
    {

        public int Id { get; set; }

       
        public int AuthorId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ReleaseDate
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Synopsis
        /// </summary>
        public string Synopsis { get; set; }

        /// <summary>
        /// Category - could become enum mapped to int val in database
        /// </summary>
        public string Category { get; set; }
    }
}
