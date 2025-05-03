using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Repositories;

namespace Uniterms.Models
{
    public class Uniterm : AlgorithmicAlgebraObject
    {
        
        public string Name { get; set ; }
        public string Value { get; set; }

        public Uniterm(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Uniterm() { }

        public override string ToString()
        {
            return Value;
        }
    }
}
