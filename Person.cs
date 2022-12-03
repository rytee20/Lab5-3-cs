using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    [Serializable]
    class Person : IDateAndCopy, IComparable, IComparable<Person>
    {
        private string _firstName;            //имя
        private string _lastName;             //фамилия
        public DateTime _dateOfBirth; //дaта рождения

        public Person() //конструктор без параметров
        {
            _firstName = "firstname";
            _lastName = "lastname";
            _dateOfBirth = new DateTime(2022, 1, 1);
        }

        public Person(string firstname, string lastname, DateTime dateofbirth) //конструктор с параметрами
        {
            this._firstName = firstname;
            this._lastName = lastname;
            this._dateOfBirth = dateofbirth;
        }

        public string FirstName //get set для имени
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string LastName //get set для фамилии
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public DateTime Date //get set для даты рождения
        {
            get
            {
                return _dateOfBirth;
            }

            set
            {
                _dateOfBirth = value;
            }
        }

        public int intDateOfBirth //int get set для даты рождения
        {
            get
            {
                return Convert.ToInt32(_dateOfBirth.Year);
            }

            set
            {
                _dateOfBirth = new DateTime(_dateOfBirth.Day, _dateOfBirth.Month, value);
            }
        }

        public override string ToString() //перегруженная версия виртуального метода tostring возвращающий строку со значениями всех полей класса
        {
            return "\nИмя: " + _firstName + ' ' + _lastName + "\nДата рождения: " + _dateOfBirth;
        }

        public string ToShortString() //виртуальный метод toshortstring возвращающий имя и фамилию
        {
            return "\nИмя: " + _firstName + ' ' + _lastName;
        }

        public override bool Equals(Object obj)//переопределение Equal
        {
            //obj.GetType().Equals(this.GetType());

            if (obj is Person)
            {
                Person personobj = (Person)obj;
                if (_firstName == personobj._firstName && _lastName == personobj._lastName && _dateOfBirth == personobj._dateOfBirth)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
            //if (object.ReferenceEquals(this, obj))
            //return true;
            //else
            //return false;
            //if (obj == null)
            //{
            //    return false;
            //}
            //Person objPers = (Person)obj;
            //if (obj as Person == null)
            //{
            //    return false;
            //}
            //return objPers._firstName == (object)_firstName && (object)objPers._lastName == (object)_lastName && (object)objPers._dateOfBirth == (object)_dateOfBirth;

        }

        public static bool operator ==(Person person1, Person person2) //перегрузка ==
        {
            //if (person1._firstName == person2._firstName && person1._lastName == person2._lastName && person1._dateOfBirth == person2._dateOfBirth)
            //return true;
            //else
            //return false;
            if (person1.Equals(person2))
                return true;
            else
                return false;
        }

        public static bool operator !=(Person person1, Person person2) //перегрузка !=
        {
            if (!person1.Equals(person2))
                return true;
            else
                return false;
        }

        public override int GetHashCode() //перегрузка GetHashCode
        {
            return HashCode.Combine(_firstName, _lastName, _dateOfBirth);
        }

        public virtual object DeepCopy()  //полная копия
        {
            Person personobj = new Person();
            personobj._firstName = _firstName;
            personobj._lastName = _lastName;
            personobj._dateOfBirth = _dateOfBirth;
            return personobj;
        }

        public int CompareTo(object o) //сравнение по фамилии обобщенной версией интерфейса
        {
            if (o is Person person) return _lastName.CompareTo(person._lastName);
            else throw new ArgumentException("Некорректное значение параметра");
        }

        public int CompareTo(Person person) //сравнение по дате рождения упрощенной версией интерфейса
        {
            if (person is null) throw new ArgumentException("Некорректное значение параметра");
            return _dateOfBirth.CompareTo(person._dateOfBirth);
        }
    }
}
