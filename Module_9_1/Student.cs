using System;

namespace Module_9_1
{
    // Класс-модель, описывающий сущность "Студент".
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public double AverageGrade { get; set; }

        // Конструктор класса. Вызывается при создании нового объекта (new Student(...)).
        public Student(int id, string name, int age, string group, double averageGrade)
        {
            Id = id;
            Name = name;
            Age = age;
            Group = group;
            AverageGrade = averageGrade;
        }
    }
}