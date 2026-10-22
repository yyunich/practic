using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Module_9_1
{
    class Program
    {
        // Статический список для хранения студентов в оперативной памяти. Static нужен, чтобы к списку можно было обращаться из любых методов без создания объекта Program.
        static List<Student> students = new List<Student>();
        static string filePath = "students.txt";

        static void Main()
        {
            // При запуске программы загружаем ранее сохраненные данные из файла
            LoadFromFile();
            
            // Бесконечный цикл для отображения меню, пока пользователь не выберет выход (0)
            while (true)
            {
                Console.WriteLine("\n=== УПРАВЛЕНИЕ СТУДЕНТАМИ ===");
                Console.WriteLine("1. Добавить\n2. Удалить\n3. Редактировать\n4. Поиск\n5. Сортировка\n6. Вывести всех\n7. Сохранить\n0. Выход");
                Console.Write("Выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": AddStudent(); break;
                    case "2": DeleteStudent(); break;
                    case "3": EditStudent(); break;
                    case "4": SearchStudents(); break;
                    case "5": SortStudents(); break;
                    case "6": PrintAll(); break;
                    case "7": SaveToFile(); break;
                    case "0": return;
                }
            }
        }

        static void AddStudent()
        {
            Console.Write("Имя: "); string name = Console.ReadLine();
            Console.Write("Возраст: "); int age = int.Parse(Console.ReadLine());
            Console.Write("Группа: "); string group = Console.ReadLine();
            Console.Write("Средний балл: "); double grade = double.Parse(Console.ReadLine());

            // Генерируем уникальный ID: находим максимальный существующий ID и прибавляем 1.
            // Если список пуст, ID будет равен 1.
            int newId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;

            // Добавляем нового студента в коллекцию
            students.Add(new Student(newId, name, age, group, grade));
            Console.WriteLine("Студент добавлен!");
        }

        static void DeleteStudent()
        {
            Console.Write("Введите ID для удаления: ");
            int id = int.Parse(Console.ReadLine());

            // Ищем студента с указанным ID с помощью LINQ (FirstOrDefault вернет null, если не найдет)
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Удалено.");
            }
            else Console.WriteLine("Не найдено.");
        }

        static void EditStudent()
        {
            Console.Write("Введите ID для редактирования: ");
            int id = int.Parse(Console.ReadLine());
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                Console.Write("Новое имя: "); student.Name = Console.ReadLine();
                Console.Write("Новый возраст: "); student.Age = int.Parse(Console.ReadLine());
                Console.Write("Новая группа: "); student.Group = Console.ReadLine();
                Console.Write("Новый балл: "); student.AverageGrade = double.Parse(Console.ReadLine());
                Console.WriteLine("Обновлено.");
            }
            else Console.WriteLine("Не найдено.");
        }

        static void SearchStudents()
        {
            Console.Write("Поиск по имени или группе: ");
            string query = Console.ReadLine().ToLower(); // Переводим запрос в нижний регистр для поиска без учета регистра

            // Фильтруем список: проверяем, содержится ли подстрока в имени ИЛИ в группе
            var result = students.Where(s => s.Name.ToLower().Contains(query) || s.Group.ToLower().Contains(query)).ToList();

            // Выводим найденные результаты
            result.ForEach(s => Console.WriteLine($"ID: {s.Id} | {s.Name} | {s.Group} | Балл: {s.AverageGrade}"));
        }

        static void SortStudents()
        {
            Console.WriteLine("1. По имени\n2. По баллу (убывание)");
            string choice = Console.ReadLine();

            // Сортируем с помощью LINQ: OrderBy (по возрастанию) и OrderByDescending (по убыванию)
            if (choice == "1") students = students.OrderBy(s => s.Name).ToList();
            else students = students.OrderByDescending(s => s.AverageGrade).ToList();
            PrintAll();
        }

        static void PrintAll()
        {
            foreach (var s in students)
                Console.WriteLine($"ID: {s.Id} | {s.Name} | Возраст: {s.Age} | Группа: {s.Group} | Балл: {s.AverageGrade}");
        }

        static void SaveToFile()
        {
            // LINQ Select преобразует объекты Student в массив строк формата "Id|Name|Age|Group|Grade"
            List<string> lines = students.Select(s => $"{s.Id}|{s.Name}|{s.Age}|{s.Group}|{s.AverageGrade}").ToList();

            // Записываем все строки в файл (перезаписывая его)
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("Сохранено в файл.");
        }

        static void LoadFromFile()
        {
            // Если файла нет (первый запуск), выходим из метода
            if (!File.Exists(filePath)) return;
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');

                // Если получилось ровно 5 частей, парсим их и создаем объект Student
                if (parts.Length == 5)
                    students.Add(new Student(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), parts[3], double.Parse(parts[4])));
            }
        }
    }
}