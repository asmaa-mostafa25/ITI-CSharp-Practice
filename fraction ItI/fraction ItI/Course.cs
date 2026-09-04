using System;
using System.Collections.Generic;
using System.Text;

namespace fraction_ItI
{
    internal class Course
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }=new List<Student>();
        public void PrintStudents()
        {
            Console.WriteLine($"Course: {Name}");
            Console.WriteLine("Students:");
            foreach (Student student in Students)
            {
                Console.WriteLine($"- {student.Name}");
            }
        }
    }
}
