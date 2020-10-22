using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    [DataContract]
    public class BaseUnitPackage
    {
        private string _name;
        private int _cost;

        [DataMember]
        public string Name { get => _name; set => _name = value; }
        [DataMember]
        public int Cost { get => _cost; set => _cost = value; }

        public BaseUnitPackage(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public BaseUnitPackage Clone()
        {
            return new BaseUnitPackage(Name, Cost);
        }
    }
}
