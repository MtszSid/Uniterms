using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Models
{
    public abstract partial class Operation : AlgorithmicAlgebraObject, INotifyPropertyChanged 
    {
        public abstract AlgorithmicAlgebraObject Left { get; set; }
        public abstract AlgorithmicAlgebraObject Right { get; set; }
        public abstract string Separator { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
