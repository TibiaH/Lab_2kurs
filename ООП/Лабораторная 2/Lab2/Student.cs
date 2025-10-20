using System;
using System.Text.RegularExpressions;

public class Student
{
    private static int objectCount = 0;
    public const string UniversName = "БГТУ";
    private readonly int id;
    private string lastName;
    private string firstName;
    private string middleName;
    private DateTime birthDate;
    private string address;
    private string phone;
    private string faculty;
    private int course;
    private string group;


    // Статический конструктор //

    static Student()
    {
        Console.WriteLine("Статический конструктор Student вызван");
    }


    // Закрытый конструктор //

    private Student(string lastName, string firstName, DateTime birthDate)
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.birthDate = birthDate;
        this.id = GenerateId();
        objectCount++;
    }


    // Конструктор без параметров //

    public Student() : this("Unknown", "Unknown", "Unknown", new DateTime(2000, 1, 1),
                         "Unknown", "Unknown", "Unknown", 1, "Unknown")
    {
    }


    // Конструктор с параметрами по умолчанию //

    public Student(string lastName = "Pavlovich", string firstName = "Dmitry",
                  DateTime? birthDate = null, string address = "Kolesnikova 49",
                  string phone = "+375 33 901 55 35", string faculty = "ИТ",
                  int course = 2, string group = "ПИ-7")
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.birthDate = birthDate ?? new DateTime(2000, 1, 1);
        this.address = address;
        this.phone = phone;
        this.faculty = faculty;
        this.course = course;
        this.group = group;
        this.id = GenerateId();
        objectCount++;
    }


    // Полный конструктор //

    public Student(string lastName, string firstName, string middleName,
                  DateTime birthDate, string address, string phone,
                  string faculty, int course, string group)
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.middleName = middleName;
        this.birthDate = birthDate;
        this.address = address;
        this.phone = phone;
        this.faculty = faculty;
        this.course = course;
        this.group = group;
        this.id = GenerateId();
        objectCount++;
    }

    // Метод для вызова закрытого конструктора //
    public static Student CreateBasic(string lastName, string firstName, DateTime birthDate)
    {
        return new Student(lastName, firstName, birthDate);
    }

    // Генерация уникального ID //
    private int GenerateId() { return objectCount + 1; }

    // Свойства с валидацией
    public int Id => id;
    public string LastName
    {
        get => lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Фамилия не может быть пустой");
            if (!Regex.IsMatch(value, @"^[А-ЯЁа-яёA-Za-z\-]+$"))
                throw new ArgumentException("Фамилия может содержать только буквы и дефисы");
            lastName = value;
        }
    }

    public string FirstName
    {
        get => firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя не может быть пустым");
            if (!Regex.IsMatch(value, @"^[А-ЯЁа-яёA-Za-z\-]+$"))
                throw new ArgumentException("Имя может содержать только буквы и дефисы");
            firstName = value;
        }
    }

    public string MiddleName
    {
        get => middleName;
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"^[А-ЯЁа-яёA-Za-z\-]+$"))
                throw new ArgumentException("Отчество может содержать только буквы и дефисы");
            middleName = value;
        }
    }

    public DateTime BirthDate
    {
        get => birthDate;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("Дата рождения не может быть в будущем");
            if (DateTime.Now.Year - value.Year > 100)
                throw new ArgumentException("Студент не может быть старше 100 лет");
            birthDate = value;
        }
    }

    public string Address
    {
        get => address;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Адрес не может быть пустым");
            address = value;
        }
    }

    public string Phone
    {
        get => phone;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Телефон не может быть пустым");
            if (!Regex.IsMatch(value, @"^[\+\-\(\)\d\s]+$"))
                throw new ArgumentException("Некорректный формат телефона");
            phone = value;
        }
    }

    public string Faculty
    {
        get => faculty;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Факультет не может быть пустым");
            faculty = value;
        }
    }

    public int Course
    {
        get => course;
        set
        {
            if (value < 1 || value > 6)
                throw new ArgumentException("Курс должен быть от 1 до 6");
            course = value;
        }
    }

    public string Group
    {
        get => group;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Группа не может быть пустой");
            group = value;
        }
    }

    // Метод расчета возраста
    public int CalculateAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    // Метод с out параметром //

    public bool TryGetFullName(out string fullName)
    {
        fullName = string.IsNullOrEmpty(middleName)
            ? $"{LastName} {FirstName}"
            : $"{LastName} {FirstName} {MiddleName}";
        return !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName);
    }


    // Метод с ref параметром //

    public void UpdateCourse(ref int newCourse, out string message)
    {
        if (newCourse < 1 || newCourse > 6)
        {
            newCourse = 1;
            message = "Курс '1' - введено некорректное значение ";
        }
        else
        {
            message = "Курс обновлен";
        }
        course = newCourse;
    }


    // Статический метод
    public static void PrintClassInfo()
    {
        Console.WriteLine($"Класс: Student, Создано объектов: {objectCount}");
    }

    // Переопределение методов Object //
    public override bool Equals(object obj)
    {
        if (obj is Student other)
        {
            return id == other.id &&
                   lastName == other.lastName &&
                   firstName == other.firstName &&
                   birthDate == other.birthDate;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, lastName, firstName, birthDate);
    }

    public override string ToString()
    {
        return $"Студент: {LastName} {FirstName} {MiddleName}, " +
               $"Возраст: {CalculateAge()}, Факультет: {Faculty}, " +
               $"Курс: {Course}, Группа: {Group}";
    }
}

