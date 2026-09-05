using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Data
{
   public class LibraryContext:DbContext
    {
        public DbSet<Models.Author> Authors { get; set; }
        public DbSet<Models.Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MS535M4\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
