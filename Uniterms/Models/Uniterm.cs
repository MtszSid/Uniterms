using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Services;

namespace Uniterms.Models
{
    internal class Uniterm : AlgorithmicAlgebraObject
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public string Value
        {
            get
            {
                return App.Services.GetService<IDataRepository>().Get(Name);
            }
            set
            {
                App.Services.GetService<IDataRepository>().AddOrUpdate(Name, value);
            }

        }

        public Uniterm(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
