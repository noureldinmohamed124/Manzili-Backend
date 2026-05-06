using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Users.Queries.GetAdminAllUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.UseCases
{
    public class GetAdminAllUsersUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminAllUsersUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<PagedResult<AdminUserDto>> ExecuteAsync(GetAdminAllUsersQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetAdminAllUsersAsync(query, cancellationToken);
        }
    }
}
