using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    public abstract class Operation : AlgorithmicAlgebraObject
    {
        public abstract AlgorithmicAlgebraObject Left { get; set; }
        public abstract AlgorithmicAlgebraObject Right { get; set; }
        public abstract string Separator { get; set; }
    }
}
