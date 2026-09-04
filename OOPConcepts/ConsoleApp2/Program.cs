
using ConsoleApp2.classs;
using System;

class Program
{
    static void Main(string[] args)
    {
        // =================================================
        // PART A
        // =================================================

        int a = 5, b = 10;

        // =========================
        // Swap by Value
        // =========================

        Console.WriteLine("=================================");
        Console.WriteLine("        SWAP BY VALUE");
        Console.WriteLine("=================================");

        Console.WriteLine($"Before swap : a = {a}, b = {b}");

        swap(a, b);

        Console.WriteLine($"After swap  : a = {a}, b = {b}");
        Console.WriteLine();


        // =========================
        // Swap by Reference
        // =========================

        Console.WriteLine("=================================");
        Console.WriteLine("      SWAP BY REFERENCE");
        Console.WriteLine("=================================");

        Console.WriteLine($"Before swap : a = {a}, b = {b}");

        swap(ref a, ref b);

        Console.WriteLine($"After swap  : a = {a}, b = {b}");
        Console.WriteLine();


        // =========================
        // Modify Array
        // =========================

        int[] arr = { 1, 2, 3 };

        Console.WriteLine("=================================");
        Console.WriteLine("        MODIFY ARRAY");
        Console.WriteLine("=================================");

        Console.WriteLine($"Before ModifyArray : arr[0] = {arr[0]}");

        ModifyArray(arr);

        Console.WriteLine($"After ModifyArray  : arr[0] = {arr[0]}");
        Console.WriteLine();


        // =========================
        // Replace Array WITHOUT ref
        // =========================

        arr = new int[] { 1, 2, 3 };

        Console.WriteLine("=================================");
        Console.WriteLine("   REPLACE ARRAY WITHOUT REF");
        Console.WriteLine("=================================");

        Console.WriteLine($"Before Replace : arr[0] = {arr[0]}");

        ReplaceArrayWithoutref(arr);

        Console.WriteLine($"After Replace  : arr[0] = {arr[0]}");
        Console.WriteLine();


        // =========================
        // Replace Array WITH ref
        // =========================

        arr = new int[] { 1, 2, 3 };

        Console.WriteLine("=================================");
        Console.WriteLine("    REPLACE ARRAY WITH REF");
        Console.WriteLine("=================================");

        Console.WriteLine($"Before Replace : arr[0] = {arr[0]}");

        ReplaceArrayWithref(ref arr);

        Console.WriteLine($"After Replace  : arr[0] = {arr[0]}");

        Console.WriteLine();


        // =================================================
        // PART B
        // =================================================

        Console.WriteLine("\n========== PART B ==========\n");


        // -------------------------------------------------
        // Create an array of 3 Book objects
        // -------------------------------------------------

        Book[] books = new Book[3];


        // -------------------------------------------------
        // Create the three Book objects
        // -------------------------------------------------

        books[0] = new Book
        {
            Title = "C# Basics",
            Author = "Ahmed",
            Price = 100,
            Copies = 5
        };

        books[1] = new Book
        {
            Title = "Clean Code",
            Author = "Robert Martin",
            Price = 250,
            Copies = 3
        };

        books[2] = new Book
        {
            Title = "Algorithms",
            Author = "Thomas Cormen",
            Price = 300,
            Copies = 4
        };


        // -------------------------------------------------
        // Print books before selling
        // -------------------------------------------------

        Console.WriteLine("Books BEFORE selling:");

        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine($"\nBook {i + 1}:");
            books[i].Display();
        }


        // -------------------------------------------------
        // Sell some books
        // -------------------------------------------------

        books[0].Sell(2);
        books[1].Sell(1);
        books[2].Sell(2);


        // -------------------------------------------------
        // Print books after selling
        // -------------------------------------------------

        Console.WriteLine("\nBooks AFTER selling:");

        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine($"\nBook {i + 1}:");
            books[i].Display();
        }


        // -------------------------------------------------
        // Reference Type Demonstration
        // -------------------------------------------------

        Console.WriteLine("\n========== REFERENCE TYPE DEMO ==========\n");

        Book first = books[0];

        Book second = first;

        Console.WriteLine(
            $"Price through first BEFORE change: {first.Price}"
        );

        second.Price = 500;

        Console.WriteLine(
            $"Price through second AFTER change: {second.Price}"
        );

        Console.WriteLine(
            $"Price through first AFTER change:  {first.Price}"
        );

        Console.WriteLine(
            "\nComment: first and second refer to the same Book object."
        );


        Console.WriteLine("\n=================================");
        Console.WriteLine("             DONE");
        Console.WriteLine("=================================");
    }


    // =================================================
    // PART A METHODS
    // =================================================

    // =========================
    // Swap by Value
    // =========================

    static void swap(int x, int y)
    {
        int temp = x;
        x = y;
        y = temp;
        Console.WriteLine($"Inside swap method: x = {x}, y = {y}");
    }


    // =========================
    // Swap by Reference
    // =========================

    static void swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }


    // =========================
    // Modify Array
    // =========================

    static void ModifyArray(int[] arr)
    {
        arr[0] = 100;
    }


    // =========================
    // Replace Array WITHOUT ref
    // =========================

    static void ReplaceArrayWithoutref(int[] arr)
    {
        arr = new int[] { 4, 5, 6 };
       
    }


    // =========================
    // Replace Array WITH ref
    // =========================

    static void ReplaceArrayWithref(ref int[] arr)
    {
        arr = new int[] { 4, 5, 6 };
    }
}

