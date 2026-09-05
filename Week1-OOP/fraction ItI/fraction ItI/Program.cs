using fraction_ItI;
class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(1, 3);

        Console.WriteLine($"{f1} + {f2} = {f1 + f2}");

        Fraction f3 = new Fraction(3, 4);
        Fraction f4 = new Fraction(1, 4);

        Console.WriteLine($"{f3} - {f4} = {f3 - f4}");

        Fraction f5 = new Fraction(2, 3);
        Fraction f6 = new Fraction(3, 4);

        Console.WriteLine($"{f5} * {f6} = {f5 * f6}");

        Fraction f7 = new Fraction(1, 2);
        Fraction f8 = new Fraction(1, 4);

        Console.WriteLine($"{f7} / {f8} = {f7 / f8}");

        Fraction f9 = new Fraction(-2, 4);

        Console.WriteLine($"{f9} = {f9} (reduced)");

        Console.WriteLine();

        // Equality
        Fraction f10 = new Fraction(1, 2);
        Fraction f11 = new Fraction(2, 4);

        Console.WriteLine($"{f10} == {f11} -> {f10 == f11}");

        // Inequality
        Console.WriteLine($"{f10} != {f11} -> {f10 != f11}");

        // Less than
        Fraction f12 = new Fraction(2, 3);

        Console.WriteLine($"{f10} < {f12} -> {f10 < f12}");

        Console.WriteLine();

        // Zero denominator
        try
        {
            Fraction invalid = new Fraction(1, 0);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("new Fraction(1, 0)");
            Console.WriteLine($"-> rejected: {ex.Message}");
        }

        Console.WriteLine();

        // Fraction.Zero
        Console.WriteLine($"Fraction.Zero -> {Fraction.Zero}");

        // Null guard
        Fraction? f = null;

        Console.WriteLine($"f == null -> {f == null} (no crash)");
        Student s1 = new Student { Name = "John Doe" };
        Student s2 = new Student { Name = "Jane Smith" };
        Course c1 = new Course { Name = "Math" };
        Course c2 = new Course { Name = "Science" };

        c1.Students.Add( s1 );
        c1.Students.Add(s2);
        c2.Students.Add(s1);
        c2.PrintStudents();


        c1.PrintStudents();
        c2.PrintStudents();


    }
}