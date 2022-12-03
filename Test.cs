using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    [Serializable]
    class Test
    {
        public string _subjectName;         //название предмета
        public bool _offset;                   //оценка

        public Test() //конструктор без параметров
        {
            _subjectName = "Предмет";
            _offset = false;
        }

        public Test(string subjectname, bool offset) //конструктор с параметрами
        {
            _subjectName = subjectname;
            _offset = offset;
        }

        public override string ToString() //перегруженная версия виртуального метода tostring возвращающий строку со значениями всех полей класса
        {
            return "\nПредмет: " + _subjectName + "\nЗачет: " + _offset;
        }
    }
}
