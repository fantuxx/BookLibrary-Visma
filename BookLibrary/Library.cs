using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace BookLibrary
{
    public class Library
    {
        public Library()
        {
            AllBooks = new List<Book>();
            Users = new List<User>();
        }

        public List<Book> AllBooks { get; set; }
        public List<User> Users { get; private set; }

        public List<Book> FilterByIsbn(string isbn) //filter-isb
        {
            var result = from book in AllBooks where book.ISBN == isbn select book;
            List<Book> byISBN = result.ToList();
            return byISBN;
        }

        public List<Book> FilterByName(string name) //filter-nam
        {
            var result = from book in AllBooks where book.Name == name select book;
            List<Book> byName = result.ToList();
            return byName;
        }

        public List<Book> FilterByAuthor(string author) //filter -aut
        {
            var result = from book in AllBooks where book.Author == author select book;
            List<Book> byAuthor = result.ToList();
            return byAuthor;
        }

        public void DisplayOptions()
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("**    q - will exit the program                **");
            Console.WriteLine("**    1 - will show all available books        **");
            Console.WriteLine("**    2 - will take a book                     **");
            Console.WriteLine("**    3 - will return a book                   **");
            Console.WriteLine("**    filter  - will enter filtering menu      **");
            Console.WriteLine("**      / aut - will filter by Authors name    **");
            Console.WriteLine("**      / nam - will filter by Books name      **");
            Console.WriteLine("**      / isb - will filter by ISBN number     **");
            Console.WriteLine("*************************************************");
        }

        public void DisplayAllBooks()
        {
            foreach (var book in AllBooks)
            {
                if (book.IsAvailable)
                {
                    Console.WriteLine("Name: {0}, Author: {1}, ISBN: {2}.", book.Name, book.Author, book.ISBN);
                }
            }
        }

        public Book FindBookByName(string name)
        {
            Book b = AllBooks.Find(c => c.Name.ToUpper().StartsWith(name.ToUpper()));
            return b;
        }

        public bool IsBookFound(string name)
        {
            if (name.Length > 3) //at least 3 chars
            {
                var book = AllBooks.Find(c => c.Name.ToUpper().StartsWith(name.ToUpper()));
                if (book != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void ReturnBook(string bookName, User user)
        {
            if (user.TakenBooks.Count == 0)
            {
                Console.WriteLine("Looks like there is nothing to return buddy");
            }
            else
            {
                var book = user.TakenBooks.Find(u => u.Name.ToUpper().Contains(bookName.ToUpper()));
                if (book != null)
                {
                    Console.WriteLine("Returning book {0}, by {1}", book.Name, book.Author);
                    Console.WriteLine();
                    user.TakenBooks.Remove(book);
                    book.IssuedTo = null;
                    book.IssuedAt = null;
                    book.IsAvailable = true;

                    Console.WriteLine("Returned succsesfuly");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Sorry cant find book. Pleas be more specific");
                }
            }
        }

        public void TakeBook(User user, string bookName, DateTime howLong)
        {
            if (IsBookFound(bookName)) //if the specified book is in the book list
            {
                var book = FindBookByName(bookName);
                Console.WriteLine("Attempting to take a book: " + book.Name);
                Console.WriteLine();
                if (user.TakenBooks.Contains(book) || user.TakenBooks.Count >= 3)
                {
                    Console.WriteLine("looks like you have this book or you reached maximum limit of 3 books");
                    Console.WriteLine();
                }
                else
                {
                    var periodDays = howLong - DateTime.Now;
                    if (book.IsAvailable)
                    {
                        if (!user.HasThisBook(book)) //if user does not have a book
                        {
                            if (periodDays.Days <= 90) //if period shorter than 90 days
                            {
                                book.IsAvailable = false;
                                user.TakenBooks.Add(book);
                                book.IssuedAt = DateTime.Now;
                                book.IssuedTo = user.Id;
                                Console.WriteLine("Book taken.");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Sorry you have to pick shorter period of time");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Looks like you already have this book");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry this book is not available right now");
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Sorry couldn't find that book. Be more clear");
            }
        }

        public void DisplayBooks(List<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine("Name: {0}, Author: {1}, ISBN: {2}.", book.Name, book.Author, book.ISBN);
            }
        }
    }
}