using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Models;

namespace Uniterms.ViewModels
{
    public class DrawViewModel
    {
        private SequenceOperation _sequence;
        private Uniterms.Models.ParallelOperaton _parallel;

        public SequenceOperation Sequence { get => _sequence;  set =>  _sequence = value;  }
        public ParallelOperaton Parallel { get => _parallel; set => _parallel = value;}
    }
}
