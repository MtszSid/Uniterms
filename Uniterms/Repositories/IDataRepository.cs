using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.Repositories
{
    internal interface IDataRepository
    {
        public bool Add(string key, string value);
        public bool Delete(string key);
        public string Get(string key);
        public bool ClearData();
        public bool AddOrUpdate(string key, string value);
    }
}
