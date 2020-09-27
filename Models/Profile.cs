using System;
using System.Collections.Generic;

namespace Library.Models
{
    public class Profile : User
    {
        public string FullName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public bool IsDebtor { get; set; } = false;
        public User User { get; set; }
        //public User User { get; set; }

        public bool CheckDebtor()
        {
            DateTime currentDate = DateTime.Today;
            if (Books != null)
            {
                foreach (var book in Books ?? new List<Book>())
                {
                    if (book.ReturnDate != null)
                    {
                        if (book.ReturnDate < currentDate)
                        {
                            this.IsDebtor = true;
                        }
                        else
                        {
                            IsDebtor = false;
                        }
                    }
                }
            }

            return IsDebtor;
        }
    }
}