using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementApp
{
    public class DataStorage
    {
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }

        private static DataStorage dataStorage;
        public static DataStorage Instance
        {
            get
            {
                if (dataStorage == null)
                    dataStorage = new DataStorage();
                return dataStorage;
            }
        }
        private DataStorage()
        {
            Students = new List<Student>();
            Groups = new List<Group>();
        }
    }
    public class Group
    {
        public string NameGroup { get; set; }
    }
    public class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public Group Groups { get; set; }
        public List<int> Marks { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Group group1 = new Group { NameGroup = "101" };
            Group group2 = new Group { NameGroup = "202" };
            Group group3 = new Group { NameGroup = "303" };

            DataStorage.Instance.Groups.Add(group1);
            DataStorage.Instance.Groups.Add(group2);
            DataStorage.Instance.Groups.Add(group3);

            Student student1 = new Student
            {
                ID = "001",
                Name = "Alexey",
                SurName = "Tipov",
                Marks = new List<int> { 4, 5, 6, 2, 4 },
                Groups = group1
            };
            Student student2 = new Student
            {
                ID = "002",
                Name = "Alesia",
                SurName = "Ribkina",
                Marks = new List<int> { 3, 8, 5, 6, 9 },
                Groups = group2
            };
            Student student3 = new Student
            {
                ID = "003",
                Name = "Ivan Repin",
                SurName = "Repin",
                Marks = new List<int> { 9, 7, 8, 10, 8 },
                Groups = group1
            };
            Student student4 = new Student
            {
                ID = "004",
                Name = "Nikolay Zorov",
                SurName = "Zorov",
                Marks = new List<int> { 3, 2, 8, 5, 5 },
                Groups = group3
            };
            Student student5 = new Student
            {
                ID = "005",
                Name = "Olia",
                SurName = "Kitova",
                Marks = new List<int> { 7, 9, 6, 6, 10 },
                Groups = group3
            };
            Student student6 = new Student
            {
                ID = "006",
                Name = "Katia",
                SurName = "Banina",
                Marks = new List<int> { 8, 5, 6, 8, 8 },
                Groups = group2
            };

            DataStorage.Instance.Students.Add(student1);
            DataStorage.Instance.Students.Add(student2);
            DataStorage.Instance.Students.Add(student3);
            DataStorage.Instance.Students.Add(student4);
            DataStorage.Instance.Students.Add(student5);
            DataStorage.Instance.Students.Add(student6);

            while (true)
            {
                Console.WriteLine("Выберите операцию: \n" +
                  "1. Вывести информацию о студентах. \n" +
                  "2. Вывести оценки студентов. \n" +
                  "3. Добавить студента. \n" +
                  "4. Редактировать студента. \n" +
                  "5. Удалить студента. \n" +
                  "6. Вывод наименований групп. \n" +
                  "7. Добавление группы. \n" +
                  "8. Выход из программы.");

                var select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        {
                            PrintStudents(DataStorage.Instance.Students);
                            break;
                        }
                    case "2":
                        {
                            PrintMarksStudents(DataStorage.Instance.Students);
                            break;
                        }
                    case "3":
                        var newStudent = NewStudent();
                        DataStorage.Instance.Students.Add(newStudent);
                        break;
                    case "4":
                        {
                            RedactStudent();
                            break;
                        }
                    case "5":
                        {
                            DeleteStudent();
                            break;
                        }
                    case "6":
                        {
                            PrintGroups(DataStorage.Instance.Groups);
                            break;
                        }
                    case "7":
                        {
                            var newGroup = InputNewGroup();
                            DataStorage.Instance.Groups.Add(newGroup);
                            break;
                        }
                    case "8":
                        {
                            return;
                        }
                }
            }
        }
        private static void PrintStudents(List<Student> students)
        {
            Console.WriteLine("Список студентов:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID:{student.ID} \n " +
                   $"Имя:{student.Name}\n" +
                   $"Фамилия:{student.SurName} \n" +
                   $"Группа:{student.Groups?.NameGroup} \n");
            }
        }
        public static void PrintMarksStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                var printMarks = string.Join(", ", student.Marks);
                Console.WriteLine($"Имя:{student.Name} {student.SurName} \n" +
                    $"Оценки: {printMarks}");
            }
        }
        private static Student NewStudent()
        {
            Console.WriteLine("Введите ID:");
            var id = Console.ReadLine();

            Console.WriteLine("Введите имя:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию:");
            var lastName = Console.ReadLine();

            Student student = new Student
            {
                ID = id,
                Name = firstName,
                SurName = lastName,
            };
            return student;
        }
        private static void RedactStudent()
        {
            PrintStudents(DataStorage.Instance.Students);

            Console.WriteLine("Введите ID:");

            var id = Console.ReadLine();

            var studentToChange = DataStorage.Instance.Students.FirstOrDefault(s => s.ID == id);

            if (studentToChange == null)
            {
                Console.WriteLine("Нет совпадений");
                return;
            }

            Console.WriteLine("Введите имя: ");
            var name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            var surName = Console.ReadLine();

            PrintGroups(DataStorage.Instance.Groups);

            Console.WriteLine("Введите номер группы: ");
            var groupName = Console.ReadLine();

            if (name != "")
                studentToChange.Name = name;

            if (surName != "")
                studentToChange.SurName = surName;

            if (groupName != "")
            {
                var newGroup = DataStorage.Instance.Groups.FirstOrDefault(g => g.NameGroup == groupName);

                if (newGroup == null)
                {
                    Console.WriteLine("Нет совпадений!");
                    return;
                }
                studentToChange.Groups = newGroup;
            }
            PrintStudents(DataStorage.Instance.Students);
        }
        private static void DeleteStudent()
        {
            PrintStudents(DataStorage.Instance.Students);
            Console.WriteLine("Введите ID:");

            var id = Console.ReadLine();

            var studentToChange = DataStorage.Instance.Students.FirstOrDefault(s => s.ID == id);
            if (studentToChange == null)
            {
                Console.WriteLine("Нет совпадений");
                return;
            }
            else
            {
                id = "";
                studentToChange.ID = id;
                string name = "";
                string surName = "";
                studentToChange.SurName = surName;
                studentToChange.Name = name;
                List<int> mark = new List<int> { 0 };
                studentToChange.Marks = mark;
            }
            PrintStudents(DataStorage.Instance.Students);
        }
        private static Group InputNewGroup()
        {
            Console.WriteLine("Введите номер группы:");
            var number = Console.ReadLine();
            var group = new Group { NameGroup = number };
            return group;
        }
        private static void PrintGroups(List<Group> groups)
        {
            Console.WriteLine("Наименование групп:");
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.NameGroup}");
            }
        }
    }
}