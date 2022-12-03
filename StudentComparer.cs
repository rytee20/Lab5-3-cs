using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    class StudentComparer: IComparer<Student>
    {
        public int Compare(Student student1, Student student2)
        {
            return student1.GPA.CompareTo(student2.GPA);
        }
    }
}
