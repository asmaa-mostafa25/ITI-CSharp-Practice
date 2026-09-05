using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.classs
{
    public class Book
    {

        // =========================
        // Fields
        // =========================

        public string Title;
        public string Author;
        public double Price;
        public int Copies;

        // =========================
        // Display
        // =========================

        public void Display()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Copies: {Copies}");
            Console.WriteLine($"Total Value: {TotalValue()}");
        }

        // =========================
        // Sell
        // =========================

        public void Sell(int n)
        {
            Copies -= n;
        }

        // =========================
        // Restock
        // =========================

        public void Restock(int n)
        {
            Copies += n;
        }

        // =========================
        // Total Value
        // =========================

        public double TotalValue()
        {
            return Price * Copies;
        }
    }


}
