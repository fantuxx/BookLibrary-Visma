using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BookLibrary
{
    public class Book
    {
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category  { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = false;
        public Guid? IssuedTo { get; set; }
        public DateTime? IssuedAt { get; set; }

        public void AboutBook()
        {
            Console.WriteLine("Book ID: {0}", this.Id);
            Console.WriteLine("Book Author: {0}", this.Author);
            Console.WriteLine("Book Name: {0}", this.Name);
            Console.WriteLine("Book Category: {0}", this.Category);
            Console.WriteLine("Book Language: {0}", this.Language);
            Console.WriteLine("Book ISBN: {0}", this.ISBN);
            Console.WriteLine("Book Published at: {0}", this.PublicationDate.ToString("d"));

            
            Console.WriteLine("Book Is Available: {0}", this.IsAvailable);
            Console.WriteLine("Book Issued to : {0}", this.IssuedTo);
            Console.WriteLine("Book Issued at : {0}", this.IssuedAt);

        }

    }
}