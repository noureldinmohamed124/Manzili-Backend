using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Admin.Users.Commands.UnBlockUser;
using Manzili.Application.Exceptions;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.UseCases
{
    public class UnblockUserUseCase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public UnblockUserUseCase(IUserRepo userRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }


        public async Task ExecuteAsync(UnblockUserCommand command, CancellationToken cancellationToken = default)
        {
            // =========================
            // Get user
            // =========================

            var user = await _userRepo.GetByIdAsync(command.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            // =========================
            // Prevent self-unblock (optional)
            // =========================

            if (user.Id == _currentUser.UserId)
                throw new ForbiddenException("You cannot unblock yourself");

            // =========================
            // Prevent admin targeting
            // =========================

            if (user.Role == UserRole.Admin)
                throw new ForbiddenException("Cannot modify admin user");

            // =========================
            // Check if currently blocked
            // =========================


            if (!user.IsCurrentlyBlocked)
                throw new BusinessRuleException("User is not currently blocked");

            // =========================
            // Unblock
            // =========================

            user.IsBlocked = false;
            user.BlockedUntil = null;
            user.BlockReason = null;
            user.BlockedByAdminId = null;

            user.UpdatedAt = DateTime.UtcNow;

            // OPTIONAL:
            // user.BlockedByAdminId = null;

            // =========================
            // Save
            // =========================

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
