using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    interface IDateAndCopy
    {
        object DeepCopy();
        DateTime Date { get; set; }
    }
}
