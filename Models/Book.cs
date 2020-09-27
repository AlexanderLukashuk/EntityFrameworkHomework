using System;
using System.Collections.Generic;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        //public List<Author> Authors { get; set; } = new List<Author>();
        public DateTime ReturnDate { get; set; }

        public void DebtСancellation()
        {
            DateTime currentDate = DateTime.Today;
            DateTime date = new DateTime(2021, 01, 01);
            
            if (ReturnDate < currentDate)
            {
                ReturnDate = date;
            }
        }
    }
}