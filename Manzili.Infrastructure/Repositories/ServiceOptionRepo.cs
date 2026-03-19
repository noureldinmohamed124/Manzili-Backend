using Manzili.Application.Abstractions.Persistence;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class ServiceOptionRepo : GenericRepo<ServiceOption>, IServiceOptionRepo
    {
        public ServiceOptionRepo(ManziliDbContext context) : base(context)
        {
        }
    }
}
