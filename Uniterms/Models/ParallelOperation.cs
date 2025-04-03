using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    public class ParallelOperation : Operation, INotifyPropertyChanged
    {
        private AlgorithmicAlgebraObject _left;
        private AlgorithmicAlgebraObject _right;
        private string _separator;
        public override AlgorithmicAlgebraObject Left
        {
            get => _left;
            set
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
                _separator = value; OnPropertyChanged();
            }
        }

        public ParallelOperation(AlgorithmicAlgebraObject left, AlgorithmicAlgebraObject right, string separator)
        {
            Left = left;
            Right = right;
            Separator = separator;
        }

        public ParallelOperation(ParallelOperation parallel)
        {
            Left = parallel.Left;
            Right = parallel.Right;
            Separator = parallel.Separator;
        }

        public override string ToString()
        {
            return _left.ToString() + " " + _separator + " " + _right.ToString();
        }
    }
}
