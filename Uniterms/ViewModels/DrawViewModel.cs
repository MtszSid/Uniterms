using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Models;
using Uniterms.Views;
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
                MementoSequence = new SequenceOperation(_sequence);

            } 
        }
        public ParallelOperation Parallel
        {
            get => _parallel;
            set
            {
                SetProperty(ref _parallel, value);
                MementoParallel = new ParallelOperation(_parallel);
            }
        }

        public SequenceOperation MementoSequence
        {
            get => _mementoSequence;
            set => SetProperty(ref _mementoSequence, value);
        }

        public ParallelOperation MementoParallel
        {
            get => _mementoParallel;
            set => SetProperty(ref _mementoParallel, value);
        }

        //public static readonly DependencyProperty ParallelProperty =
        //    DependencyProperty.Register(
        //        nameof(Parallel),
        //        typeof(ParallelOperation),
        //        typeof(DrawViewModel),
        //        new PropertyMetadata(null, OnOperationChanged));


        public void SetLeftOfParallel()
        {
            
            if(Parallel is null)
            {
                return;
            }
            if(Parallel.Left is Uniterm)
            {
                Parallel.Left = _sequence;
                Parallel.Right = _mementoParallel.Right;
                OnPropertyChanged(nameof(Parallel));
            }
        }

        public void SetRightOfParallel()
        {

            if (Parallel is null)
            {
                return;
            }
            if (Parallel.Left is Uniterm)
            {
                Parallel.Left = _mementoParallel.Left;
                Parallel.Right = _mementoParallel.Right;
                OnPropertyChanged(nameof(Parallel));
            }
        }

        public void NewParallel(string left, string right, string separator)
        {
            Parallel = new ParallelOperation(new Uniterm(left), new Uniterm(right), separator);
        }

        public void NewSequence(string left, string right, string separator)
        {
            Sequence = new SequenceOperation(new Uniterm(left), new Uniterm(right), separator);
        }
    }
}
