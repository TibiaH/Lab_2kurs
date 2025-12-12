class Program
{
    static void Main()
    {
        File.Delete("info.txt");
        
        Console.WriteLine(Reflector.GetAssemblyName("Student"));
        Console.WriteLine(Reflector.HasPublicConstructors("Student"));
        
        Console.WriteLine("Методы:");
        foreach (var m in Reflector.GetPublicMethods("Student"))
            Console.WriteLine(m);
        
        Console.WriteLine("Интерфейсы:");
        foreach (var i in Reflector.GetImplementedInterfaces("Student"))
            Console.WriteLine(i);
        
        Console.WriteLine("Методы с string:");
        foreach (var m in Reflector.GetMethodsByParameterType("Student", typeof(string)))
            Console.WriteLine(m);
        
        var student = Reflector.Create<Student>();
        Console.WriteLine($"Student: {student.Name}");
        Reflector.Invoke(student, "Display", null);
        
        // Teacher
        Console.WriteLine("\n--- Teacher ---");
        var teacher = Reflector.Create<Teacher>();
        Console.WriteLine($"Teacher: {teacher.Name}");
        
        // System.String
        Console.WriteLine("\n--- System.String ---");
        Console.WriteLine(Reflector.GetAssemblyName("System.String"));
        
        // InvokeFromFile
        File.WriteAllText("params.txt", "Group-2");
        Reflector.InvokeFromFile("Student", "Update");
        }
}