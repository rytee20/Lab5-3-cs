using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    class TestCollections
    {
        private System.Collections.Generic.List<Person> _listOfPersons = new List<Person>();
        private System.Collections.Generic.List<string> _listOfStrings= new List<string>();
        private System.Collections.Generic.Dictionary<Person, Student> _dictionaryPersonStudent = new Dictionary<Person, Student>();
        private System.Collections.Generic.Dictionary<string, Student> _dictionarStringStudent = new Dictionary<string, Student>();

        public List<Person> ListOfPersons 
        { 
            get 
            { 
                return _listOfPersons; 
            } 
            set 
            { 
                _listOfPersons = value; 
            } 
        }

        public List<string> ListOfStrings { 
            get { 
                return _listOfStrings; 
            } 
            set 
            { 
                _listOfStrings = value; 
            } 
        }

        public Dictionary<Person, Student> DictionaryPersonStudent 
        { 
            get 
            { 
                return _dictionaryPersonStudent; 
            } 
            set { 
                _dictionaryPersonStudent = value; 
            } 
        }

        public Dictionary<string, Student> DictionarStringStudent 
        { 
            get 
            { 
                return _dictionarStringStudent; 
            } 
            set 
            { 
                _dictionarStringStudent = value; 
            } 
        }

        public TestCollections(int value) //конструктор
        {
            for (int i = 0; i < value; i++)
            {
                _listOfPersons.Add(new Person("Имя" + i.ToString(), "Фамилия" + i.ToString(), new DateTime(2000, 1, 1)));
                _dictionaryPersonStudent.Add(new Person("Имя" + i.ToString(), "Фамилия" + i.ToString(), new DateTime(2000, 1, 1)), GenerateStudent(i));
                _listOfStrings.Add(i.ToString());
                _dictionarStringStudent.Add(i.ToString(), GenerateStudent(i));
            }
        }

        public static Student GenerateStudent(int value) //генерация студента
        {
            Student newStudent = new Student("Имя" + value.ToString(), "Фамилия" + value.ToString(), new DateTime(2000, 1, 1), 11, Education.Specialist);
            return newStudent;
        }

        //public T KeyByValue<T, W>(this Dictionary<T, W> dict, W val) //функция для поиска ключа по значению
        //{
            //T key = default;
            //foreach (KeyValuePair<T, W> pair in dict)
            //{
                //if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                //{
                    //key = pair.Key;
                    //break;
                //}
            //}
            //return key;
        //}

        public void TimeSearch(string str1, Person person, Student student)
        {
            Student value;

            DateTime start_time = DateTime.Now;
            //int start_time = Environment.TickCount; //поиск по списку персон
            ListOfPersons.BinarySearch(person);
            //var person2 = ListOfPersons.First(o => o.Equals(person));
            //int end_time= Environment.TickCount;
            DateTime end_time = DateTime.Now;
            Console.WriteLine("Время поиска по списку персон: "+(end_time - start_time).ToString());

            start_time = DateTime.Now; //поиск по списку стрингов
            ListOfStrings.BinarySearch(str1);
            end_time = DateTime.Now;
            Console.WriteLine("Вемя поиска по списку строк: " + (end_time - start_time).ToString());

            start_time = DateTime.Now; //поиск в словаре персона-студент по ключу
            if (_dictionaryPersonStudent.TryGetValue(person, out value))
            {
                Student stud = value;
                //Console.WriteLine(stud);
            }
            end_time = DateTime.Now;
            Console.WriteLine("Время поиска в словаре персона-студент по ключу: " + (end_time - start_time).ToString());

            //start_time = Environment.TickCount; //поиск в словаре персона-студент по значению
            //Person pers = _dictionaryPersonStudent.Where(x=>x.Value == student).FirstOrDefault().Key;
            //end_time = Environment.TickCount;
            //Console.WriteLine("Время поиска в словаре персона-студент по значению: " + (end_time - start_time).ToString());

            start_time = DateTime.Now; //поиск в словаре строка-студент по ключу
            if (_dictionarStringStudent.TryGetValue(str1, out value))
            {
                Student stud = value;
                //Console.WriteLine(stud);
            }
            end_time = DateTime.Now;
            Console.WriteLine("Время поиска в словаре строка-студент по ключу: " + (end_time - start_time).ToString());

            //start_time = Environment.TickCount; //поиск в словаре строка-студент по значению
            //string str2 = _dictionarStringStudent.Where(x => x.Value == student).FirstOrDefault().Key;
            //end_time = Environment.TickCount;
            //Console.WriteLine("Время поиска в словаре строка-студент по значению: " + (end_time - start_time).ToString());

        }
    }
}
