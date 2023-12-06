using System;
using System.Collections;
using System.Collections.Generic;

namespace lab1
{

    class Program
    {
        static void Main(string[] args)
        {

            //1
            Console.WriteLine("\nСоздать объект типа T с непустым списком элементов, для которого предусмотрен ввод данных с консоли." +
                "Создать полную копию объекта спомощью метода, использующего сериализацию, и вывести исходный объект и его копию.");

            Student student1 = new Student("Мельпомена", "Сурана", new DateTime(2003, 6, 7), 21, Education.Bachelor);
            Exam exam = new Exam("Математика", 5, new DateTime(2022, 6, 18));
            student1.AddExams(exam);
            exam = new Exam("Физика", 5, new DateTime(2022, 6, 12));
            student1.AddExams(exam);
            exam = new Exam("Иностранный язык", 5, new DateTime(2022, 6, 11));
            student1.AddExams(exam);
            exam = new Exam("ПрЧМИ", 5, new DateTime(2022, 6, 24));
            student1.AddExams(exam);
            Test test = new Test("Физ-ра", false);
            student1.AddTests(test);
            test = new Test("Физика", true);
            student1.AddTests(test);
            test = new Test("Иностранный язык", true);
            student1.AddTests(test);
            test = new Test("ПрЧМИ", true);
            student1.AddTests(test);

            Console.WriteLine("\nИсходный объект:\n"+student1.ToString());

            //2
            Console.WriteLine("\nПредложить пользователю ввести имя файла." +
                "если файла с введенным именем нет, приложение должно сообщить об этом и создать файл; " +
                "если файл существует, вызвать метод Load(string filename) дляинициализации объекта T данными из файла.");

            Console.WriteLine("Введите название файла");
            string file_name="";
            try
            {
                file_name = Console.ReadLine();
                int point = 0;
                for (int i = 0; i < file_name.Length; i++)
                {
                    if (file_name[i] == '.')
                        point++;
                }
                if (point < 0)
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("Название файла неверно.");
            }
            finally
            {
                if (System.IO.File.Exists("H:\\" + file_name))
                {
                    student1.Load("H:\\" + file_name);
                }
                else
                {
                    var fs = System.IO.File.Create("H:\\" + file_name);
                    fs.Dispose();
                }
            }

            //3
            Console.WriteLine("\nВывести объект T.");
            Console.WriteLine("\nСчитали:\n" + student1.ToString());

            //4
            Console.WriteLine("\nДля этого же объекта T сначала вызвать метод AddFromConsole(), затем метод Save(string filename).Вывести объект T.");
            student1.AddFromConsole();
            student1.Save("Student1Save.dat");
            Console.WriteLine("\nВывод:\n" + student1.ToString());

            //5
            Console.WriteLine("");
            Student.Load(file_name, student1);
            student1.AddFromConsole();
            Student.Save(file_name, student1);
        }
    }
}
