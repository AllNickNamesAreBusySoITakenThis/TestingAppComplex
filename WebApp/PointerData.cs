using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class PointerData
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double East { get; set; }
        public double North { get; set; }
        public double Hight { get; set; }
        public double Value { get; set; }
        public PointerData(string _name, string _descr, double _east, double _north, double _hight, double _value)
        {
            Name = _name;
            Description = _descr;
            East = _east;
            North = _north;
            Hight = _hight;
            Value = _value;
        }
    }
}
