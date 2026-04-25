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
    public class PaymentProofRepo : GenericRepo<PaymentProof>, IPaymentProofRepo
    {
        public PaymentProofRepo(ManziliDbContext context) : base(context)
        {
        }
    }
}
