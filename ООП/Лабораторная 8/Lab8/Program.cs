using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

public class Director
{
    public event Action<decimal> OnRaise;
    public event Action<decimal> OnPenalty;
    
    public void RaiseSalary(decimal amount) => OnRaise?.Invoke(amount);
    public void ApplyPenalty(decimal amount) => OnPenalty?.Invoke(amount);
}

public abstract class Employee
{
    public string Name { get; }
    public decimal Salary { get; protected set; }
    
    protected Employee(string name, decimal salary) => (Name, Salary) = (name, salary);
    
    public void DisplayInfo() => Console.WriteLine($"{Name}: {Salary:C}");
}

public class Turner : Employee
{
    public Turner(string name, decimal salary) : base(name, salary) { }
    
    public void Subscribe(Director director)
    {
        director.OnRaise += amount => Salary += amount;
        director.OnPenalty += amount => Salary -= amount;
    }
}

public class Student : Employee
{
    public Student (string name, decimal salary) : base(name, salary) {}
    public void Subscribe(Director director, bool onlyRaise = false)
    {
        director.OnRaise += amount => Salary += amount;
        if (!onlyRaise) 
            director.OnPenalty += amount => Salary -= amount;
    }
}

public class StringProcessing
{
    public static string RemoveElements(string s) => string.Concat(s.Where(c => !",.?!;:-()[]{}'\"".Contains(c)));
    public static string Add(string s) => $"*** {s} ***";
    public static string ToUpper(string s) => s.ToUpper();
    public static string RemoveSpaces(string s) => string.Join(" ", s.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    public static string Reverse(string s)
    {
        var words= s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(words);
        return string.Join(" ", words);
    }
    public static bool ContainsLetter(string s) => s.Any(char.IsLetter);
}

class Program
{
    static void Main()
    {
        var director = new Director();
        var turner1 = new Turner("Токарь1", 3000);
        var turner2 = new Turner("Токарь2", 3500);

        var student1 = new Student("Студент1", 200);
        var student2 = new Student("Студент2", 250);

        turner1.Subscribe(director);
        turner2.Subscribe(director);
        student1.Subscribe(director);
        student2.Subscribe(director, onlyRaise: true);

        director.RaiseSalary(200);
        director.ApplyPenalty(100);

        turner1.DisplayInfo();
        turner2.DisplayInfo();
        student1.DisplayInfo();
        student2.DisplayInfo();

        var test = " Hello,,,[{?}] World";
        Predicate<string> validator = StringProcessing.ContainsLetter;
        Func<string, string>[] pipeline =
        {
            StringProcessing.RemoveElements,
            StringProcessing.RemoveSpaces,
            StringProcessing.ToUpper,
            StringProcessing.Add,
            StringProcessing.Reverse
        };

        string result = test;
        foreach( var func in pipeline) 
        result = func(result);

        Console.WriteLine(result);
    }
}