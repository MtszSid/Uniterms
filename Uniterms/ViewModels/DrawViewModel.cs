﻿using System.ComponentModel;
using System;
using Uniterms.Models;

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
            set
            {
                SetProperty(ref _sequence, value);
            }
        }
        public ParallelOperation Parallel
        {
            get => _parallel;
            set
            {
                if (_parallel != value)
                {
                    if(_parallel is not null)
                    {
                        _parallel.PropertyChanged -= Parallel_PropertyChanged;
                    }
                    SetProperty(ref _parallel, value);
                    if (_parallel is not null)
                    {
                        _parallel.PropertyChanged += Parallel_PropertyChanged;
                    }
                }
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

        public void SetLeftOfParallel()
        {

            if (MementoSequence is null || Parallel is null)
            {
                return;
            }

            Parallel.Left = MementoSequence;
            Parallel.Right = MementoParallel.Right;
            Sequence = null;
            
        }

        public void SetRightOfParallel()
        {

            if (MementoSequence is null || Parallel is null)
            {
                return;
            }

            Parallel.Left = MementoParallel.Left;
            Parallel.Right = MementoSequence;
            Sequence = null;
            
        }

        public void NewParallel(string left, string right, string separator)
        {
            Parallel = new ParallelOperation(new Uniterm(left), new Uniterm(right), separator);
            MementoParallel = new ParallelOperation(new Uniterm(left), new Uniterm(right), separator);
            Sequence = MementoSequence;
        }

        public void NewSequence(string left, string right, string separator)
        {
            Sequence = new SequenceOperation(new Uniterm(left), new Uniterm(right), separator);
            MementoSequence = new SequenceOperation(new Uniterm(left), new Uniterm(right), separator);
            Parallel = new ParallelOperation(MementoParallel);
        }

        private void Parallel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Parallel));
        }
    }
}
