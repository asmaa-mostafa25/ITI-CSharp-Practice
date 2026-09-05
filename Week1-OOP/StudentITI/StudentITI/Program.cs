using StudentITI.Class;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Creating students...");

        Student[] students =
        {
            new Student("Mona Adel", 3.8, "mona@gmail.com"),
            new Student("Omar Hany", 2.9, "omar@gmail.com"),
            new Student("Ali Kamal", 1.7, "ali@gmail.com")
        };


        foreach (Student student in students)
        {
            student.Display();
        }


        Console.WriteLine();
        Console.WriteLine("-- validation proof --");


        // Invalid Name
        try
        {
            students[0].Name = "";
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Name = \"\"        -> rejected");
        }


        // Invalid GPA
        try
        {
            students[0].Gpa = 4.5;
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Gpa = 4.5        -> rejected");
        }


        // Invalid Email
        try
        {
            students[0].Email = "no-at";
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Email = \"no-at\"  -> rejected");
        }


        // Id is read-only
        Console.WriteLine("Id = 99           -> will not compile");


        Console.WriteLine();
        Console.WriteLine($"Student.Count -> {Student.Count}");

        Console.WriteLine(
            $"Initials(\"Mona Adel\") -> {students[0].Initials}"
        );


        Console.WriteLine();
        Console.WriteLine("-- reference check --");

        Student a = students[0];

        Student b = a;

        b.Promote(4.0);

        Console.WriteLine($"a.Gpa -> {a.Gpa:F2}  // same object");
    }
}