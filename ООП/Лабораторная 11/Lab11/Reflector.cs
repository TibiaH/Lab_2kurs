using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Reflector
{
    private static string file = "info.txt";
    
    // a
    public static string GetAssemblyName(string className)
    {
        var result = Type.GetType(className).Assembly.GetName().Name;
        File.AppendAllText(file, $"Сборка: {result}\n");
        return result;
    }
    
    // b
    public static bool HasPublicConstructors(string className)
    {
        var result = Type.GetType(className).GetConstructors().Any();
        File.AppendAllText(file, $"Конструкторы: {result}\n");
        return result;
    }
    
    // c
    public static IEnumerable<string> GetPublicMethods(string className)
    {
        var methods = Type.GetType(className).GetMethods().Select(m => m.Name).Distinct().ToList();
        File.AppendAllText(file, $"Методы: {string.Join(", ", methods)}\n");
        return methods;
    }
    
    // d
    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        var type = Type.GetType(className);
        var fields = type.GetFields().Select(f => f.Name);
        var properties = type.GetProperties().Select(p => p.Name);
        var result = fields.Concat(properties).ToList();
        File.AppendAllText(file, $"Поля и свойства: {string.Join(", ", result)}\n");
        return result;
    }
    
    // e
    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        var interfaces = Type.GetType(className).GetInterfaces().Select(i => i.Name).ToList();
        File.AppendAllText(file, $"Интерфейсы: {string.Join(", ", interfaces)}\n");
        return interfaces;
    }
    
    // f
    public static IEnumerable<string> GetMethodsByParameterType(string className, Type parameterType)
    {
        var methods = Type.GetType(className).GetMethods()
            .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
            .Select(m => m.Name)
            .ToList();
        File.AppendAllText(file, $"Методы с {parameterType.Name}: {string.Join(", ", methods)}\n");
        return methods;
    }
    
    // g1
    public static object Invoke(object obj, string methodName, object[] parameters)
    {
        return obj.GetType().GetMethod(methodName).Invoke(obj, parameters);
    }
    
    // g2
    public static object InvokeFromFile(string className, string methodName)
    {
        var type = Type.GetType(className);
        var obj = Activator.CreateInstance(type);
        var method = type.GetMethod(methodName);
        
        if (File.Exists("params.txt"))
        {
            var lines = File.ReadAllLines("params.txt");
            var parameters = method.GetParameters();
            var args = new object[parameters.Length];
            
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = Convert.ChangeType(lines[i], parameters[i].ParameterType);
            }
            
            return method.Invoke(obj, args);
        }
        else
        {
            var parameters = method.GetParameters();
            var args = parameters.Select(p => GetValue(p.ParameterType)).ToArray();
            return method.Invoke(obj, args);
        }
    }
    
    // 2
    public static T Create<T>() where T : class
    {
        var type = typeof(T);
        var constructor = type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        var args = parameters.Select(p => GetValue(p.ParameterType)).ToArray();
        return (T)constructor.Invoke(args);
    }
    
    private static object GetValue(Type type)
    {
        if (type == typeof(string)) return "Test";
        if (type == typeof(int)) return 100;
        return Activator.CreateInstance(type);
    }
}