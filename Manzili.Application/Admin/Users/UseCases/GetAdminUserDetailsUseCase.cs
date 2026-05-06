using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Users.Queries.GetAdminUserById;
using Manzili.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.UseCases
{
    public class GetAdminUserDetailsUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminUserDetailsUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }


        public async Task<AdminUserDetailsDto> ExecuteAsync(GetAdminUserDetailsQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _adminRepo
                .GetUserDetailsByIdAsync(query.UserId, cancellationToken);

            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }
    }
}
