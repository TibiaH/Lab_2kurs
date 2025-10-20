using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Student student1 = new Student("Фамилия", "Имя", new DateTime(2005, 6, 10), "ул.Ленина", "+375339015535", "ИТ", 2, "ПИ - 7");
        var student2 = new Student();
        Student[] students = { student1, student2 };
        student1.Address = "ул.Тихая";
        student1.LastName = "Homan";
        var anonym = new
        {
            Name = student1.LastName + " " + student1.FirstName,
            Age = student1.CalculateAge()
        };
        Console.WriteLine($"Анонимный: {anonym}");

        student1.TryGetFullName(out string fullName);
        Console.WriteLine(fullName);

        int newCourse = 9;
        student1.UpdateCourse(ref newCourse, out string message);
        Console.WriteLine(newCourse);

    }
}