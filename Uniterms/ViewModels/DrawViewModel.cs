using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Models;
using Windows.Services.Maps;

namespace Uniterms.ViewModels
{
    public class DrawViewModel : BaseViewModel
    {
        private SequenceOperation _sequence;
        private ParallelOperation _parallel;
        private SequenceOperation _mementoSequence;
        private ParallelOperation _mementoParallel;

        public SequenceOperation Sequence
        { 
            get => _sequence;  
            set { 
                SetProperty(ref _sequence, value);
                _mementoSequence = new SequenceOperation(_sequence);

            } 
        }
        public ParallelOperation Parallel { get => _parallel; set => SetProperty(ref _parallel, value);}

        public void SetLeftOfParallel()
        {
            
            if(Parallel is null)
            {
                return;
            }
            if(Parallel.Left is Uniterm)
            {
                
            }
        }

        public void NewParallel(string left, string right)
        {
            Parallel = new ParallelOperation(new Uniterm(left), new Uniterm(right));
        }

        public void NewSequence(string left, string right)
        {
            Sequence = new SequenceOperation(new Uniterm(left), new Uniterm(right));
        }
    }
}
