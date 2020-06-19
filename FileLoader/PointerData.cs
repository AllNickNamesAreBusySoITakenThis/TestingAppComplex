using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader
{
    public class PointerData
    {
        [BsonId]
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
        public static PointerData GetPointerData(Dictionary<string,object> dict)
        {
            try
            {
                return new PointerData()
                {
                    Name = dict["NAME"].ToString(),
                    Description = dict["DESCRIPTION"].ToString(),
                    East = Convert.ToDouble(dict["EAST"].ToString().Replace(".", ",")),
                    North = Convert.ToDouble(dict["NORTH"].ToString().Replace(".", ",")),
                    Hight = Convert.ToDouble(dict["HIGHT"].ToString().Replace(".", ",")),
                    Value = Convert.ToDouble(dict["VALUE"].ToString().Replace(".", ","))
                };
            }
            catch 
            {
                return null;
            }
        }
    }
}
