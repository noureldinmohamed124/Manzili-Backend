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
    public class OrderRepo : GenericRepo<Transaction>, IOrderRepo
    {
        public OrderRepo(ManziliDbContext context) : base(context)
        {
        }
    }
}
