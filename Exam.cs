using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    [Serializable]
    class Exam:IDateAndCopy
    {
        public string _subjectName;         //название предмета
        public int _mark;                   //оценка
        public System.DateTime _dateOfExam; //дата экзамена

        public Exam() //конструктор без параметров
        {
            _subjectName = "Предмет";
            _mark = 0;
            _dateOfExam = new DateTime(2022, 1, 1);
        }

        public DateTime Date //get set для даты экзамена
        {
            get
            {
                return _dateOfExam;
            }

            set
            {
                _dateOfExam = value;
            }
        }

        public Exam(string subjectname, int mark, DateTime dateofexam) //конструктор с параметрами
        {
            this._subjectName = subjectname;
            this._mark = mark;
            this._dateOfExam = dateofexam;
        }

        public override string ToString() //перегруженная версия виртуального метода tostring возвращающий строку со значениями всех полей класса
        {
            return "\nПредмет: " + _subjectName + "\nОценка: " + _mark + "\nДата экзамена: " + _dateOfExam;
        }

        public object DeepCopy()  //полная копия
        {
            Exam examobj = new Exam();
            examobj._subjectName = _subjectName;
            examobj._mark = _mark;
            examobj._dateOfExam = _dateOfExam;
            return examobj;
        }
    }
}
