using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_11
{
    public enum Days 
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    public class Film
    {
        public string? Name { get; set; }
        public int CountSeria { get; set; }
        public int Duration { get; set; }
        public List<Days>? Day { get; set; }
        public int getPeriod() => Duration * CountSeria;

    }
}
