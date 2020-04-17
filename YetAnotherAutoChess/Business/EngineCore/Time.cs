using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public static class Time
    {
        public static double TimeDifferenceFromNowInSeconds(DateTime time)
        {
            return (DateTime.Now - time).TotalSeconds;
        }
    }
}
