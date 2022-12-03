using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab1
{
    [Serializable]
    enum Education
    {
        Specialist,
        Bachelor,
        SecondEducation
    }

    [Serializable]
    class Student: Person, IDateAndCopy, IEnumerable//, ISerializable
    {
        public IEnumerator Enumerator { get; set; }

        public IEnumerator GetEnumerator()
        {
            return Enumerator;
        }

        private Person _studentData;          //данные о студенте
        private Education _degreeOfEducation; //форма обучения
        private int _groupNumber;             //номер группы
        private List<Test> _listOfTests = new List<Test>();
        private List<Exam> _listOfExams = new List<Exam>();


        public Student(string firstname, string lastname, DateTime dateofbirth, int groupnumber, Education degreeofeducation) //конструктор с параметрами
            :base(firstname, lastname, dateofbirth)
        {
            _studentData = new Person();
            _studentData.FirstName = firstname;
            _studentData.LastName = lastname;
            _studentData.Date = dateofbirth;
            _groupNumber = groupnumber;
            _degreeOfEducation = degreeofeducation;
        }

        public Student():this("Имя","Фамилия", new DateTime (2022, 1, 1), 1, Education.Specialist) // конструктор без парамеров
        {
        } 

        public Person StudentData //get set
        {
            get
            {
                return _studentData;
            }

            set
            {
                _studentData = value;
            }
        }

        public Education DegreeOfEducation //get set для формы обучения
        {
            get
            {
                return _degreeOfEducation;
            }

            set
            {
                _degreeOfEducation = value;
            }
        }

        public int GroupNumber //get set для номера группы
        {
            get
            {
                return _groupNumber;
            }
            set
            {
                if (value <= 100 || value > 599)
                {
                    throw new ArgumentOutOfRangeException("Неверное значение!");
                }
                else
                {
                    _groupNumber = value;
                }
            }
        }

        public List<Exam> Exams //get set для экзаменов
        {
            get
            {
                return _listOfExams;
            }

            set
            {
                _listOfExams = value;
            }
        }

        public List<Test> Tests //get set для экзаменов
        {
            get
            {
                return _listOfTests;
            }

            set
            {
                _listOfTests = value;
            }
        }

        public double GPA //подсчет среднего балла
        {
            get
            {
                if (_listOfExams == null)
                    return 0;
                int sum = 0;
                int count = 0;
                foreach (Exam exam in _listOfExams)
                {
                    sum = sum + exam._mark;
                    count = count + 1;
                }
                return (double)sum / _listOfExams.Count;
            }
        }

        public bool this [Education index] //индексатор
        {
            get
            {
                if (_degreeOfEducation == index)
                    return true;
                else return false;
                //return DegreeOfEducation == index;
            }
        }

        public void AddExams (params Exam[] exams) //добавление экзаменов
        {
            _listOfExams.AddRange(exams);
        }

        public void AddTests(params Test[] tests) //добавление экзаменов
        {
            _listOfTests.AddRange(tests);
        }

        public override string ToString() //преобразование в строку со спистом тестов и экзаменв
        {
            string exams="";
            if (_listOfExams.Count == 0)
                exams = "нет сданных экзаменов";
            else
            {
                foreach (Exam exam in _listOfExams)
                {
                    exams += exam.ToString();
                }
            }

            string tests = "";
            if (_listOfTests.Count == 0)
                tests = "нет сданных зачетов";
            else
            {
                foreach (Test test in _listOfTests)
                {
                    tests += test.ToString();
                }
            }

            return "\nДанные студента: " + "\nИмя: " + FirstName + ' ' + LastName + "\nДата рождения: " + Date + "\nФорма обучения: " + _degreeOfEducation + "\nНомер группы: " + _groupNumber + "\nСданные экзамены: " + exams + "\nСданные зачеты: " + tests;
        }

        public new string ToShortString() //преобразование в строку со средним баллом без списка тестов и экзаменов
        {
            return "\nДанные студента: " + "\nИмя: " + FirstName + ' ' + LastName + "\nДата рождения: " + Date + "\nФорма обучения: " + _degreeOfEducation + "\nНомер группы: " + _groupNumber + "\nСредний балл: " + GPA;
        }

        public override object DeepCopy()  //полная копия с сериализацией
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream("Copy.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, this);
            }
            //Console.WriteLine("--> Сохранение объекта в Binary format");

            using (Stream fStream = File.OpenRead("Copy.dat"))
            {
                Student student_copy = (Student)binFormat.Deserialize(fStream);
                return student_copy;
            }
        }

        public bool Save(string filename)  //сохранение данных объекта с помощью сереализации
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, this.StudentData.FirstName);
                    binFormat.Serialize(fStream, this.StudentData.LastName);
                    binFormat.Serialize(fStream, this.StudentData._dateOfBirth);
                    binFormat.Serialize(fStream, this.DegreeOfEducation);
                    binFormat.Serialize(fStream, this.GroupNumber);
                    binFormat.Serialize(fStream, this._listOfExams);
                    binFormat.Serialize(fStream, this._listOfTests);
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Ошибка сереализации");
                return false;
            }
        }

        public bool Load(string filename)  //инициализация объекта с помощью сереализации
        {
            if (System.IO.File.Exists(filename))
            {
                try
                {
                    BinaryFormatter binFormat = new BinaryFormatter();
                    using (Stream fStream = File.OpenRead(filename))
                    {
                        string name = (string)binFormat.Deserialize(fStream);
                        StudentData.FirstName = name;
                        FirstName = name;
                        string lastname = (string)binFormat.Deserialize(fStream);
                        StudentData.LastName = lastname;
                        LastName = lastname;
                        DateTime date = (DateTime)binFormat.Deserialize(fStream);
                        StudentData.Date = date;
                        Date = date;
                        DegreeOfEducation = (Education)binFormat.Deserialize(fStream);
                        int gn = (int)binFormat.Deserialize(fStream);
                        GroupNumber = gn;
                        _listOfExams = (List<Exam>)binFormat.Deserialize(fStream);
                        _listOfTests = (List<Test>)binFormat.Deserialize(fStream);
                    }
                    return true;
                }
                catch
                {
                    Console.WriteLine("Ошибка десериализации");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Такого файла не существует");
                return false;
            }
        }

        public bool AddFromConsole() //добавить экзамен с консоли
        {
            Console.WriteLine("Ввод нового экзамена");
            Exam exam = new Exam();

            while (true)
            {
                try {
                    Console.WriteLine("Введите название предмета: ");
                    exam._subjectName = Console.ReadLine();
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели неверно! Ввод завершен.");
                    return false;
                }

            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите оценку за экзамен: ");
                    exam._mark = int.Parse(Console.ReadLine());
                    if (exam._mark>5||exam._mark<1)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели неверно! Ввод завершен.");
                    return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Оценка может быть от 1 до 5! Ввод завершен.");
                    return false;
                }

            }

            int day, mounth, year;

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите год проведения экзамена: ");
                    year = int.Parse(Console.ReadLine());
                    if (year > 2022 || year < 2000)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели неверно! Ввод завершен.");
                    return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Год может быть от 2000 до 2022! Ввод завершен.");
                    return false;
                }

            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите месяц проведения экзамена: ");
                    mounth = int.Parse(Console.ReadLine());
                    if (mounth < 1 || mounth > 12)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели неверно! Ввод завершен.");
                    return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Месяц может быть от 1 до 12! Ввод завершен.");
                    return false;
                }

            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите день проведения экзамена: ");
                    day = int.Parse(Console.ReadLine());
                    if (mounth == 2 && year%4==0 && (day<1||day>29))
                    {
                        throw new Exception();
                    }
                    else
                    if (mounth == 2 && year % 4 != 0 && (day < 1 || day > 28))
                    {
                        throw new Exception();
                    }
                    else
                    if ((mounth == 4 || mounth==6 || mounth==9 || mounth==11) && (day<1||day>30))
                    {
                        throw new Exception();
                    }
                    else
                        if ((mounth == 1 || mounth == 3 || mounth == 5 || mounth == 7 || mounth == 8|| mounth == 10|| mounth == 12) && (day < 1 || day > 31))
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели неверно! Ввод завершен.");
                    return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("День не соответствует месяцу! Ввод завершен.");
                    return false;
                }

            }

            exam._dateOfExam = new DateTime(year,mounth,day);

            this.AddExams(exam);

            return true;
        }

        public static bool Save(string filename, Student student) //сохранение всего объекта в файл
        {
            try 
            { 

                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, student);
                }
                return true;
            }
            catch 
            {
                Console.WriteLine("Ошибка сереализации");
                return false;
            }
        }

        public static bool Load(string filename, Student student)
        {
            if (System.IO.File.Exists(filename))
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                try
                {
                    using (Stream fStream = File.OpenRead(filename))
                    {
                        Student student_copy = (Student)binFormat.Deserialize(fStream);
                    }
                    return true;
                }
                catch
                {
                    Console.WriteLine("Ошибка десериализации");
                    return false;
                }

            }
            else
            {
                Console.WriteLine("Такого файла не существует");
                return false;
            }
        }

        public IEnumerable<Object> AllExamsAndTests() //список всех экзаменов и тестов
        {
            Console.WriteLine("\nСписок всех экзаменов:");
            for (int i = 0; i < _listOfExams.Count; i++)
            {
                yield return (Exam)_listOfExams[i];
                Console.WriteLine(((Exam)_listOfExams[i]).ToString());
            }
            Console.WriteLine("\nСписок всех тестов:");
            for (int i = 0; i < _listOfTests.Count; i++)
            {
                yield return (Test)_listOfTests[i];
                Console.WriteLine(((Test)_listOfTests[i]).ToString());
            }
        }

        public IEnumerable<Exam> ExamsWithMark(int mark) //экзамены с оценкой выше заданной
        {
            for (int i = 0; i < _listOfExams.Count; i++)
            {
                if(((Exam)_listOfExams[i])._mark>mark)
                {
                    yield return (Exam)_listOfExams[i];
                    Console.WriteLine(((Exam)_listOfExams[i]).ToString());
                }
            }
        }

        public IEnumerable<Object> PassedExamsAndTests() //итератор для сданных экзаменов и тестов
        {
            Console.WriteLine("\nСписок сданных экзаменов:");
            for (int i = 0; i < _listOfExams.Count; i++)
            {
                if(((Exam)_listOfExams[i])._mark>=2)
                {
                    yield return (Exam)_listOfExams[i];
                    Console.WriteLine(((Exam)_listOfExams[i]).ToString());
                }
            }
            Console.WriteLine("\nСписок сданных тестов:");
            for (int i = 0; i < _listOfTests.Count; i++)
            {
                if(((Test)_listOfTests[i])._offset==true)
                {
                    yield return (Test)_listOfTests[i];
                    Console.WriteLine(((Test)_listOfTests[i]).ToString());
                }
            }
        }

        public IEnumerable<Object> PassedTests() //итератор для сданных тестов для которых сдан экзамен
        {
            Console.WriteLine("\nСписок сданных тестов для которых сдан экзамен:");
            for (int i = 0; i < _listOfTests.Count; i++)
            {
                if (((Test)_listOfTests[i])._offset == true)
                {
                    for (int j = 0; j < _listOfExams.Count; j++)
                    {
                        if (((Exam)_listOfExams[j])._subjectName== ((Test)_listOfTests[i])._subjectName && ((Exam)_listOfExams[j])._mark >= 2)
                        {
                            yield return (Test)_listOfTests[i];
                            Console.WriteLine(((Test)_listOfTests[i]).ToString());
                        }
                    }    
                }
            }
        }

        protected void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TestProperty", this);
        }
    }
}
