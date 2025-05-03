using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.DB;
using Windows.System.UserProfile;

namespace Uniterms.Services
{
    internal class DbRepostory : IDataRepository
    {
        public bool Add(string key, string value)
        {
            if (key is null || value is null)
                return false;
            using (var context = new AppDbContextFactory().CreateDbContext([]))
            {
                var get = context.Singleton.Where(p => p.Key == key).SingleOrDefault();
                if (get is not default(Singleton))
                {
                    return false;
                }
                context.Singleton.Add(new Singleton() { Key = key, Value = value });
                var res = context.SaveChanges();
                return res > 0;
            }
        }

        public bool AddOrUpdate(string key, string value)
        {
            if (key is null || value is null)
                return false;

            using (var context = new AppDbContextFactory().CreateDbContext([]))
            {
                var get = context.Singleton.Where(p => p.Key == key).SingleOrDefault();
                if (get is default(Singleton))
                {
                    context.Singleton.Add(new Singleton() { Key = key, Value = value });
                }
                else
                {
                    get.Value = value;
                    context.Singleton.Update(get);
                }
                var res = context.SaveChanges();
                return res > 0;
            }
        }

        public bool ClearData()
        {
            using (var context = new AppDbContextFactory().CreateDbContext([]))
            {
                var get = from i in context.Singleton select i;
                foreach (var item in get) 
                {
                    context.Singleton.Remove(item);
                }
                return context.SaveChanges() > 0;
            }
        }

        public bool Delete(string key)
        {
            if (key is null)
                return false;
            using (var context = new AppDbContextFactory().CreateDbContext([]))
            {
                var value = context.Singleton.Where(p =>  p.Key == key).SingleOrDefault();
                context.Singleton.Remove(value);
                var res = context.SaveChanges();
                return res > 0;
            }
        }

        public string Get(string key)
        {
            if (key is null)
                return null;
            using (var context = new AppDbContextFactory().CreateDbContext([]))
            {
                var value = context.Singleton.Where(p => p.Key == key).SingleOrDefault();
                return value.Value;
            }
        }
    }
}
