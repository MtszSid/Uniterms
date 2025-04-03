using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    public class SequenceOperation : Operation
    {
        private AlgorithmicAlgebraObject _left;
        private AlgorithmicAlgebraObject _right;
        private string _separator;

        public override AlgorithmicAlgebraObject Left
        {
            get => _left; set
            {
                _left = value;
                OnPropertyChanged();
            }
        }
        public override AlgorithmicAlgebraObject Right
        {
            get => _right;
            set
            {
                _right = value;
                OnPropertyChanged();
            }
        }
        public override string Separator
        {
            get => _separator;
            set
            {
                _separator = value;
                OnPropertyChanged();
            }
        }

        public SequenceOperation(AlgorithmicAlgebraObject left, AlgorithmicAlgebraObject right, string separator)
        {
            Left = left;
            Right = right;
            Separator = separator;
        }

        public SequenceOperation(SequenceOperation other)
        {
            Left = other.Left;
            Right = other.Right;
            Separator = other.Separator;
        }

        public override string ToString()
        {
            return _left.ToString() + " " + _separator + " " + _right.ToString();
        }
    }
}
