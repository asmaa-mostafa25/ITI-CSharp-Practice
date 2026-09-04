using System;
using System.Collections.Generic;
using System.Text;

namespace fraction_ItI
{
    internal class Student
    {
        public string Name { get; set; }
        public List<Course> Courses { get; set; }=new List<Course>();

        public void PrintCourses()
        {
            Console.WriteLine($"Student: {Name}");
            Console.WriteLine("Courses:");
            foreach (Course course in Courses)
            {
                Console.WriteLine($"- {course.Name}");
            }
        }
    }
}
