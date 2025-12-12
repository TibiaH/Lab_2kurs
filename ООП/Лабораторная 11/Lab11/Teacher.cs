public class Teacher
{
    public string Name { get; set; }
    public string Subject { get; set; }
    
    public Teacher() { }
    public Teacher(string name, string subject) 
    { 
        Name = name; 
        Subject = subject; 
    }
    
    public void Teach()
    {
        Console.WriteLine($"{Name} teaches {Subject}");
    }
}