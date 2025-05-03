using System.ComponentModel;
using Uniterms.Models;

namespace Uniterms.ViewModels
{
    public class DrawViewModel : BaseViewModel
    {
        private SequenceOperation _sequence;
        private ParallelOperation _parallel;
        private SequenceOperation _mementoSequence;
        private ParallelOperation _mementoParallel;

        private string _state;

        private readonly string _parallelLeftKey = "ParallelLeft";
        private readonly string _parallelRightKey = "ParallelRight";
        private readonly string _sequenceLeftKey = "SequenceLeft";
        private readonly string _sequenceRightKey = "SequenceRight";

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
                    if (_parallel is not null)
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

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
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
            if( string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            {
                State = "Unitermy dla operacji zrównoleglania nie mogą być puste.";
                return;
            }
            Parallel = new ParallelOperation(new Uniterm(_parallelLeftKey, left), new Uniterm(_parallelRightKey, right), separator);
            MementoParallel = new ParallelOperation(new Uniterm(_parallelLeftKey, left), new Uniterm(_parallelRightKey, right), separator);
            Sequence = MementoSequence;
        }

        public void NewSequence(string left, string right, string separator)
        {
            if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            {
                State = "Unitermy dla operacji sekwencjonowania nie mogą być puste.";
                return;
            }
            Sequence = new SequenceOperation(new Uniterm(_sequenceLeftKey, left), new Uniterm(_sequenceRightKey, right), separator);
            MementoSequence = new SequenceOperation(new Uniterm(_sequenceLeftKey, left), new Uniterm(_sequenceRightKey, right), separator);
            if(MementoParallel is not null)
                Parallel = new ParallelOperation(MementoParallel);
        }

        private void Parallel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Parallel));
        }
    }
}
