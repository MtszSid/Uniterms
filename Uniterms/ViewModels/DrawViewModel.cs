using System.ComponentModel;
using Uniterms.Models;
using Uniterms.Services;

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
        private readonly string _parallelSeparatorKey = "ParallelSeparator";

        private readonly string _sequenceLeftKey = "SequenceLeft";
        private readonly string _sequenceRightKey = "SequenceRight";
        private readonly string _sequenceSeparatorKey = "SequenceSeparator";


        public SequenceOperation Sequence
        {
            get => _sequence;
            set
            {
                if (_sequence != value)
                {
                    if (_sequence is not null)
                    {
                        _sequence.PropertyChanged -= Sequence_PropertyChanged;
                    }
                    SetProperty(ref _sequence, value);
                    if (_sequence is not null)
                    {
                        _sequence.PropertyChanged += Sequence_PropertyChanged;
                    }
                }
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
                return;

            Parallel.Left = MementoSequence;
            Parallel.Right = OperationsService.RestoreUniterm(_parallelRightKey);
            Sequence = null;

        }

        public void SetRightOfParallel()
        {

            if (MementoSequence is null || Parallel is null)
                return;

            Parallel.Left = OperationsService.RestoreUniterm(_parallelLeftKey);
            Parallel.Right = MementoSequence;
            Sequence = null;

        }

        public void NewParallel(string left, string right, string separator)
        {
            if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            {
                State = "Unitermy dla operacji zrównoleglania nie mogą być puste.";
                return;
            }
            Parallel = OperationsService.CreateParallel(_parallelLeftKey, left, _parallelRightKey, right, _parallelSeparatorKey, separator);
            MementoParallel = OperationsService.RestoreParallel(_parallelLeftKey, _parallelRightKey, _parallelSeparatorKey);
            Sequence = OperationsService.RestoreSequence(_sequenceLeftKey, _sequenceRightKey, _sequenceSeparatorKey);
        }

        public void NewSequence(string left, string right, string separator)
        {
            if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            {
                State = "Unitermy dla operacji sekwencjonowania nie mogą być puste.";
                return;
            }
            Sequence = OperationsService.CreateSequence(_sequenceLeftKey, left, _sequenceRightKey, right, _sequenceSeparatorKey, separator);
            MementoSequence = OperationsService.RestoreSequence(_sequenceLeftKey, _sequenceRightKey, _sequenceSeparatorKey);
            Parallel = OperationsService.RestoreParallel(_parallelLeftKey, _parallelRightKey, _parallelSeparatorKey);
        }

        private void Parallel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Parallel));
        }

        private void Sequence_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Sequence));
        }
    }
}
