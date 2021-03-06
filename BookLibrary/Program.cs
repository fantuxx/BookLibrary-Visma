using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace BookLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new LoadJson<Book>();
            var library = new Library();
            library.AllBooks = loader.LoadFromFile("books.json");//populating with data //Might wanna change for your suit
            string name;
            string lastName;



            Console.WriteLine("*****Hello and welcome to console Visma's book library *****");
          
            Console.WriteLine("Nice. Please enter required information for user registration");
            Console.WriteLine("What is your name?");
            name = Console.ReadLine();
            Console.WriteLine("What is your lastname");
            lastName = Console.ReadLine();
            var user = new User(name, lastName);
            Console.WriteLine("Registration completed");
            Console.WriteLine("Please pick an option");
            library.DisplayOptions();
            string cont = null;
            cont = Console.ReadLine();

            while (cont != "q")
            {
                switch (cont)
                {
                    case "0":
                        library.DisplayOptions();
                        cont = Console.ReadLine();
                        break;
                    case "1":
                        library.DisplayAllBooks();
                        goto case "0";
                    case "2":
                        Console.WriteLine("What is the name of the book?");
                        string nameOfTheBook = Console.ReadLine();
                        library.TakeBook(user, nameOfTheBook, DateTime.Now);
                        goto case "0";

                    case "3":
                        Console.WriteLine("Which book you want to return?  Specify a name");
                        user.DisplayMyBooks();
                        string retunName = Console.ReadLine();
                        library.ReturnBook(retunName,user);
                        goto case "0";

                    case "filter /nam":
                        Console.WriteLine("Filtering by Name");
                        Console.WriteLine("Please provide a name");
                        string namName = Console.ReadLine();
                        Console.WriteLine();
                        var namBooks = library.FilterByIsbn(namName);
                        library.DisplayBooks(namBooks);
                        goto case "0";

                    case "filter /isb":
                        Console.WriteLine("Filtering by ISBN");
                        Console.WriteLine("Please provide a ISBN number");
                        string isbName = Console.ReadLine();
                        Console.WriteLine();
                        var isbBooks = library.FilterByIsbn(isbName);
                        library.DisplayBooks(isbBooks);
                        goto case "0";

                    case "filter /aut":
                        Console.WriteLine("Filtering by authors name");
                        Console.WriteLine("Please provide a authors name");
                        string autName = Console.ReadLine();
                        Console.WriteLine();
                        library.FilterByName(autName);
                        var autBooks = library.FilterByIsbn(autName);
                        library.DisplayBooks(autBooks);
                        goto case "0";

                    case "q":
                        Console.WriteLine("you are quitting");
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        cont = Console.ReadLine();
                        break;
                }
            }
            
                
            



        }


    }
}
