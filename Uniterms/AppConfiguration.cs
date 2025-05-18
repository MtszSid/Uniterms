using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterms.Repositories;
using Uniterms.Services;

namespace Uniterms
{
    public static class AppConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataRepository, DbRepostory>();
            services.AddSingleton<IOperationsService, OperationsService>();
        }
    }
}
