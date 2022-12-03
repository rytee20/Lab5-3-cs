using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lab1
{
    class StudentCollection
    {
        private List<Student> _students = new List<Student>();

        public List<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
            }
        }

        public void AddDefaults() //добавить некоторое число элементов типа Student для инициализации коллекции по умолчанию
        {
            for (int i = 0; i < 2; i++)
            {
                _students.Add(new Student());
            }
        }

        public void AddStudents(params Student[] students) //добавление элементов
        {
            _students.AddRange(students);
        }

        public override string ToString() //перегруженный метод ToString
        {
            string StudentsString = "";
            foreach (Student student in _students)
            {
                StudentsString = StudentsString + student.ToString() + "\n";
            }
            return StudentsString;
        }

        public string ToShortString() //перегруженный метод ToShortString
        {
            string StudentsString = "";
            foreach (Student student in _students)
            {
                StudentsString = StudentsString + student.ToShortString() + "\n";
            }
            return StudentsString;
        }

        public void SortByLastName() //сортировка по фамилии
        {
            _students.Sort((x, y) => x.LastName.CompareTo(y.LastName));
        }

        public void SortByDateOfBirth() //сортировка по дате рождения
        {
            _students.Sort();
        }

        public void SortByGPA() //сортировка по среднему баллу
        {
            StudentComparer comp = new StudentComparer();
            _students.Sort(comp);
        }

        public double MaxGPA //максимальный средний балл
        {
            get
            {
                if (_students.Count == 0)
                {
                    return 0;
                }
                return _students.Max(student => student.GPA);
            }
        }

        public IEnumerable<Student> GetSpecialists //находим специалистов
        {
            get
            {
                IEnumerable<Student> Specialists = _students.Where(student => student.DegreeOfEducation == Education.Specialist);
                return Specialists;
            }
        }

        public List<Student> AverageMarkGroup(double value) //находим студентов с заданным средним баллом
        {
            IEnumerable<IGrouping<double,Student>> StudentsWithGPA = _students.GroupBy(student => student.GPA);
            foreach (IGrouping<double, Student> student in StudentsWithGPA)
            {
                if (student.Key == value)
                {
                    return student.ToList<Student>();
                }
            }
            return null;
        }
    }
}
