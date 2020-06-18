using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader
{
    public class PointerData
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double East { get; set; }
        public double North { get; set; }
        public double Hight { get; set; }
        public double Value { get; set; }

        public PointerData()
        {

        }
        public PointerData(string _name, string _descr, double _east, double _north, double _hight, double _value)
        {
            Name = _name;
            Description = _descr;
            East = _east;
            North = _north;
            Hight = _hight;
            Value = _value;
        }
        public PointerData(Dictionary<string,object> dict)
        {
            double _east = 0;
            double _north = 0;
            double _hight = 0;
            double _value = 0;
            Name = dict["NAME"].ToString();
            Description = dict["DESCRIPTION"].ToString();
            Double.TryParse(dict["EAST"].ToString().Replace(".",","),out _east);
            Double.TryParse(dict["NORTH"].ToString().Replace(".", ","), out _north);
            Double.TryParse(dict["HIGHT"].ToString().Replace(".", ","), out _hight);
            Double.TryParse(dict["VALUE"].ToString().Replace(".", ","), out _value);
            East = _east;
            North = _north;
            Hight = _hight;
            Value = _value;
        }
    }
}
