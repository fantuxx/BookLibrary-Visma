using System;
using System.Collections.Generic;

namespace BookLibrary
{
    public class User
    {
        public List<Book> TakenBooks { get; set; }

        public User(string name, string lastName)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Id = Guid.NewGuid();
            TakenBooks = new List<Book>(); //Initializing new list in which take books will be stored
        }
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }

        public void HowManyBooksUserHas()
        {
            if (TakenBooks.Count >0)
            {
                Console.WriteLine("User Has taken {0} books", TakenBooks.Count);
            }
            else
            {
                Console.WriteLine("User has none books take");
            }
        }

        public void AboutUser()
        {
            Console.WriteLine("Users full name {0} {1}", this.Name, this.LastName);
            Console.WriteLine("Users Id {0}", this.Id);
            Console.WriteLine("Now you have total {0} books taken", TakenBooks.Count);
        }
        public void DisplayMyBooks()
        {
            foreach (var book in TakenBooks)
            {
                Console.WriteLine("Name: {0}, Author: {1}, ISBN: {2}.", book.Name, book.Author, book.ISBN);
            }
        }

        public bool HasThisBook(Book book)
        {
            return this.TakenBooks.Contains(book);
        }
    }
}