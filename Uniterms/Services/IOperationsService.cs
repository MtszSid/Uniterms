using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Models;
using Uniterms.Repositories;

namespace Uniterms.Services
{
    public interface IOperationsService
    {
        public ParallelOperation CreateParallel(string leftUnitermKey, string leftUniterm, string rightUnitermKey, string rightUniterm, string separatorKey, string separator);
        public SequenceOperation CreateSequence(string leftUnitermKey, string leftUniterm, string rightUnitermKey, string rightUniterm, string separatorKey, string separator);
        public  ParallelOperation RestoreParallel(string leftUnitermKey, string rightUnitermKey, string separatorKey);
        public SequenceOperation RestoreSequence(string leftUnitermKey, string rightUnitermKey, string separatorKey);
        public Uniterm RestoreUniterm(string key);
        public string RestoreSeparator(string key);
    }
}
