using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DateModifier
{
    public int DaysDifference(string date1, string date2)
    {
        var firstDate = Convert.ToDateTime(date1);
        var secondDate = Convert.ToDateTime(date2);
        int difference = Math.Abs((firstDate - secondDate).Days);

        return difference;
    }
}
