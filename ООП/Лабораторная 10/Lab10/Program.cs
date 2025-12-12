using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Student
{
    public string LastName {get; set;}
    public string FirstName {get; set;}
    public int Age {get; set;}
    public int Course {get; set;}
    public string Faculty {get; set;}
    public string Group {get; set;}
    public string Specialty {get; set;}
    public double AverageGrade {get; set;}
    public bool IsActive {get; set;}
    public override string ToString()
    {
        return $"{LastName} {FirstName}, возраст - {Age}, специальность - {Specialty}, факультет - {Faculty}, группа - {Group}, курс - {Course}, средний балл - {AverageGrade}";
    }
}
public class Department
{
    public string FacultyName {get; set;}
    public int RoomNumber {get; set;}
}


class Program
{
    static void Main()
    {
        //1.1
        string[] months = {"June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November"};
        int n = 5;
        var length = months.Where(m => m.Length == n);
        foreach (var month in length) Console.WriteLine(month);
        
        Console.WriteLine("---------------");

        //1.2
        var summeWinter = months.Where(m => m == "June" || m == "July" || m == "August" || m == "December" || m == "January" || m == "February");
        foreach (var month in summeWinter) Console.WriteLine(month);

        Console.WriteLine("---------------");

        //1.3
        var order = months.OrderBy(m => m);
        foreach (var month in order) Console.WriteLine(month);

        Console.WriteLine("---------------");

        //1.4
        Console.WriteLine(months.Count(m => m.Contains("u") && m.Length >= 4));

        Console.WriteLine("---------------");

        //2
        List<Student> students = new List<Student>
        {
            new Student {LastName = "Homan1", FirstName = "Dmitry1", Age = 18, Course = 2, Faculty = "ИТ", Group = "ПИ-1", Specialty = "програмная инженерия", AverageGrade = 7, IsActive = false},
            new Student {LastName = "Homan2", FirstName = "Dmitry2", Age = 19, Course = 3, Faculty = "ИТ", Group = "ПИ-7", Specialty = "програмная инженерия", AverageGrade = 8, IsActive = true},
            new Student {LastName = "Homan3", FirstName = "Dmitry3", Age = 19, Course = 3, Faculty = "ЛХ", Group = "ПИ-2", Specialty = "програмная инженерия", AverageGrade = 7, IsActive = true},
            new Student {LastName = "Homan4", FirstName = "Dmitry4", Age = 20, Course = 4, Faculty = "ИТ", Group = "ПИ-7", Specialty = "програмная инженерия", AverageGrade = 7, IsActive = true},
            new Student {LastName = "Homan5", FirstName = "Dmitry5", Age = 18, Course = 2, Faculty = "ХП", Group = "ПИ-7", Specialty = "програмная инженерия", AverageGrade = 5, IsActive = false},
            new Student {LastName = "Homan6", FirstName = "Dmitry6", Age = 18, Course = 2, Faculty = "ИТ", Group = "ПИ-4", Specialty = "програмная инженерия", AverageGrade = 7, IsActive = true},
            new Student {LastName = "Homan7", FirstName = "Dmitry7", Age = 17, Course = 1, Faculty = "ХП", Group = "ПИ-10", Specialty = "програмная инженерия", AverageGrade = 3, IsActive = true},
            new Student {LastName = "Homan8", FirstName = "Dmitry8", Age = 18, Course = 2, Faculty = "ИТ", Group = "ПИ-8", Specialty = "програмная инженерия", AverageGrade = 2, IsActive = false},
            new Student {LastName = "Homan9", FirstName = "Dmitry9", Age = 18, Course = 2, Faculty = "ХП", Group = "ПИ-6", Specialty = "програмная инженерия", AverageGrade = 9, IsActive = true}
        };

        Console.WriteLine("-------- Задание 1 --------");

        var specialtyStudents = students.Where(s => s.Specialty == "програмная инженерия").OrderBy(s => s.LastName);
        foreach (var student in specialtyStudents) Console.WriteLine(student);
        
        Console.WriteLine("-------- Задание 2 --------");

        var groupStudent = students.Where(s => s.Faculty == "ИТ" && s.Group == "ПИ-7");
        foreach (var student in groupStudent) Console.WriteLine(student);

        Console.WriteLine("-------- Задание 3 --------");

        var youngestStudent = students.OrderBy(s => s.Age).FirstOrDefault();
        Console.WriteLine(youngestStudent);

        Console.WriteLine("-------- Задание 4 --------");

        string targetGroup = "ПИ-7";
        var groupOrderedStudent = students.Where(s => s.Group == targetGroup).OrderBy(s => s.LastName);
        foreach (var student in groupOrderedStudent) Console.WriteLine(student);

        Console.WriteLine("-------- Задание 5 --------");

        string targetName = "Dmitry3";
        var firstStudent = students.Where(s => s.FirstName == targetName);
        Console.WriteLine(firstStudent);
       
        Console.WriteLine("-------- Собственные запросы --------");

        var query = students.Where(s=> s.IsActive && s.AverageGrade >= 7).GroupBy(s => s.Faculty)
        .Select(g => new
        {
            StudentCount = g.Count(),
            AverageAge = g.Average(s => s.Age),
            TopStudent = g.OrderByDescending(s => s.AverageGrade).ThenBy(s => s.LastName).FirstOrDefault(),
            AllHaveGoodGrades = g.All(s => s.AverageGrade >= 6.0)
        })
        .OrderByDescending(x => x.AverageAge)
        .Take(2);

        foreach (var facultyStats in query)
        {
            Console.WriteLine($"Количество студентов - {facultyStats.StudentCount}");
            Console.WriteLine($"Средний возраст - {facultyStats.AverageAge}");
            Console.WriteLine($"Балл выше 6 {facultyStats.AllHaveGoodGrades}");
            Console.WriteLine($"Лучший студент - {facultyStats.TopStudent?.LastName}");
        }

        // Использование Join
        List<Department> departments = new List<Department>
        {
            new Department { FacultyName = "ИТ", RoomNumber = 101},
            new Department { FacultyName = "ЛХ", RoomNumber = 205},
            new Department { FacultyName = "789", RoomNumber = 310}
        };

        var joinQuery = students
            .Join(departments,
                student => student.Faculty,
                department => department.FacultyName,
                (student, department) => new
                {
                    StudentName = $"{student.LastName} {student.FirstName}",
                    StudentGroup = student.Group,
                    Faculty = student.Faculty,
                    DepartmentRoom = department.RoomNumber,
                    AverageGrade = student.AverageGrade
                })
            .OrderBy(x => x.Faculty)
            .ThenByDescending(x => x.AverageGrade);

             Console.WriteLine("------ Задание с Join ------");
        
        foreach (var item in joinQuery)
        {
            Console.WriteLine($"{item.StudentName} - {item.StudentGroup} - {item.Faculty} - {item.DepartmentRoom}");
        }
    }
}