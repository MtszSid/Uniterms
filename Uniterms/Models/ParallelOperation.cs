using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    public class ParallelOperation : Operation
    {
        private AlgorithmicAlgebraObject _left;
        private AlgorithmicAlgebraObject _right;

        public override AlgorithmicAlgebraObject Left { get => _left; set => _left = value; }
        public override AlgorithmicAlgebraObject Right { get => _right; set => _right = value; }

        public ParallelOperation(AlgorithmicAlgebraObject left, AlgorithmicAlgebraObject right)
        {
            Left = left;
            Right = right;
        }
    }
}
