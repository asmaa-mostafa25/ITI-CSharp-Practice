using System;
using System.Collections.Generic;
using System.Text;

namespace StudentITI.Class
{
    public class Student
    {
        // =========================================
        // Static fields
        // =========================================

        private static int nextId = 81001;

        public static int Count { get; private set; }


        // =========================================
        // Fields
        // =========================================

        private string name;
        private double gpa;
        private string email;


        // =========================================
        // Properties
        // =========================================

        // Id: Read-only from outside
        public int Id { get; }


        // Name
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");

                name = value.Trim();
            }
        }


        // GPA
        public double Gpa
        {
            get
            {
                return gpa;
            }

            set
            {
                if (value < 0 || value > 4)
                    throw new ArgumentException("GPA must be between 0 and 4.");

                gpa = value;
            }
        }


        // Email
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    !value.Contains("@") ||
                    value.Contains(" "))
                {
                    throw new ArgumentException("Invalid email.");
                }

                email = value.Trim();
            }
        }


        // =========================================
        // Computed Properties
        // =========================================

        public string Status
        {
            get
            {
                if (Gpa >= 3.5)
                    return "Excellent";

                if (Gpa >= 2.0)
                    return "Good";

                return "At Risk";
            }
        }


        public string Initials
        {
            get
            {
                return GetInitials(Name);
            }
        }


        // =========================================
        // Constructors
        // =========================================

        // 1. Full Constructor
        public Student(string name, double gpa, string email)
        {
            Id = nextId++;

            Name = name;
            Gpa = gpa;
            Email = email;

            Count++;
        }


        // 2. Name Constructor
        public Student(string name)
            : this(name, 0, "unknown@example.com")
        {
        }


        // 3. Constructor with chained this
        public Student()
            : this("Unknown Student", 0, "unknown@example.com")
        {
        }


        // =========================================
        // Methods
        // =========================================

        public void Promote(double newGpa)
        {
            Gpa = newGpa;
        }


        public void Display()
        {
            Console.WriteLine(
                $"{Id}  {Name}  {Gpa:F2}  {Status}"
            );
        }


        // =========================================
        // Helper Method
        // =========================================

        private static string GetInitials(string fullName)
        {
            string[] parts = fullName.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
            );

            string result = "";

            foreach (string part in parts)
            {
                result += char.ToUpper(part[0]) + ".";
            }

            return result;
        }
    }
}
