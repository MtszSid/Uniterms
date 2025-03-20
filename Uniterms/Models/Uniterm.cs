using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    internal class Uniterm : AlgorithmicAlgebraObject
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }

        public Uniterm(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
