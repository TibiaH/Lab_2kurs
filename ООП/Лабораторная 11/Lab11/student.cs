public interface IStudent
{
    string Name { get; set; }
    void Display();
}

public class Student : IStudent
{
    public string Name { get; set; }
    public string Group { get; set; }
    public int Age { get; set; }

    public Student() { }
    public Student(string name) { Name = name; }
    public Student(string name, string group) { Name = name; Group = group; }

    public void Display()
    {
        Console.WriteLine($"{Name}, {Group}");
    }
    
    public void Update(string group)
    {
        Group = group;
    }
}