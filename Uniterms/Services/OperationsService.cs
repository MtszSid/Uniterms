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
    public static class OperationsService
    {
        public static ParallelOperation CreateParallel(string leftUnitermKey, string leftUniterm, string rightUnitermKey, string rightUniterm, string separatorKey, string separator)
        {
            var left = new Uniterm(leftUnitermKey, leftUniterm);
            var right = new Uniterm(rightUnitermKey, rightUniterm);

            var repository = App.Services.GetService<IDataRepository>();
            repository.AddOrUpdate(left.Name, left.Value);
            repository.AddOrUpdate(right.Name, right.Value);
            repository.AddOrUpdate(separatorKey, separator);

            return new ParallelOperation(left, right, separator);

        }

        public static  SequenceOperation CreateSequence(string leftUnitermKey, string leftUniterm, string rightUnitermKey, string rightUniterm, string separatorKey, string separator)
        {
            var left = new Uniterm(leftUnitermKey, leftUniterm);
            var right = new Uniterm(rightUnitermKey, rightUniterm);

            var repository = App.Services.GetService<IDataRepository>();
            repository.AddOrUpdate(left.Name, left.Value);
            repository.AddOrUpdate(right.Name, right.Value);
            repository.AddOrUpdate(separatorKey, separator);

            return new SequenceOperation(left, right, separator);
        }

        public static ParallelOperation RestoreParallel(string leftUnitermKey, string rightUnitermKey, string separatorKey)
        {
            var left = RestoreUniterm(leftUnitermKey);
            var right = RestoreUniterm(rightUnitermKey);
            var separator = RestoreSeparator(separatorKey);

            if (string.IsNullOrEmpty(separator) || left is null || right is null)
                return null; 

            return new ParallelOperation(left, right, separator);
        }

        public static SequenceOperation RestoreSequence(string leftUnitermKey, string rightUnitermKey, string separatorKey)
        {
            var left = RestoreUniterm(leftUnitermKey);
            var right = RestoreUniterm(rightUnitermKey);
            var separator = RestoreSeparator(separatorKey);

            if (string.IsNullOrEmpty(separator) || left is null || right is null)
                return null;

            return new SequenceOperation(left, right, separator);
        }

        public static Uniterm RestoreUniterm(string key)
        {
            var repository = App.Services.GetService<IDataRepository>();
            var uniterm = repository.Get(key);

            if (uniterm is null)
                return null;
            return new Uniterm(key, uniterm);
        }

        public static string RestoreSeparator(string key)
        {
            var repository = App.Services.GetService<IDataRepository>();
            return repository.Get(key);
        }
    }
}
