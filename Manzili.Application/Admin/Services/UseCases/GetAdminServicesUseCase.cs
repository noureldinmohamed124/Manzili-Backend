using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Services.Queries.GetAdminServices;
using Manzili.Application.Admin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Services.UseCases
{
    public class GetAdminServicesUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminServicesUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }


        public async Task<PagedResult<AdminServiceDto>> ExecuteAsync(GetAdminServicesQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetServicesAsync(query, cancellationToken);
        }
    }
}
