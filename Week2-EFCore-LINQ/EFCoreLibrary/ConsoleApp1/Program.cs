using System;
using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        // Create a new instance of the LibraryContext
        // to connect to the Library database
        using var context = new LibraryContext();

        // Create a new Author with multiple Books
        var author = new Author
        {
            Name = "John Doe",

            // Add four books for the author
            Books = new List<Book>
            {
                new Book { Title = "C# Programming", Price = 29.99m },
                new Book { Title = "Entity Framework Core", Price = 39.99m },
                new Book { Title = "ASP.NET Core", Price = 49.99m },
                new Book { Title = "Blazor", Price = 59.99m }
            }
        };

        // Add the author and his books to the database
        context.Authors.Add(author);

        // Save the changes to the database
        context.SaveChanges();

        Console.WriteLine("Author and books added to the database.");

        // Retrieve all authors along with their books
        // using Eager Loading with Include()
        var authors = context.Authors
            .Include(a => a.Books)
            .ToList();

        // Display authors and their books
        foreach (var a in authors)
        {
            Console.WriteLine($"Author: {a.Name}");

            foreach (var b in a.Books)
            {
                Console.WriteLine($"  Book: {b.Title}, Price: {b.Price}");
            }
        }
    }
}