using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Admin.Users.Commands.BlockUser;
using Manzili.Application.Exceptions;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.UseCases
{
    public class BlockUserUseCase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public BlockUserUseCase(IUserRepo userRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task ExecuteAsync(BlockUserCommand command, CancellationToken cancellationToken = default)
        {
            // =========================
            // Validate input
            // =========================

            if (string.IsNullOrWhiteSpace(command.Reason))
                throw new BusinessRuleException("Block reason is required");

            if (command.BlockedUntil.HasValue && command.BlockedUntil <= DateTime.UtcNow)
            {
                throw new BusinessRuleException("BlockedUntil must be in the future");
            }

            // =========================
            // Get user
            // =========================

            var user = await _userRepo.GetByIdAsync(command.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            // =========================
            // Prevent self-block
            // =========================

            if (user.Id == _currentUser.UserId)
                throw new ForbiddenException("You cannot block yourself");

            // =========================
            // Prevent blocking admin
            // =========================

            if (user.Role == UserRole.Admin)
                throw new ForbiddenException("Cannot block another admin");

            // =========================
            // Already blocked?
            // =========================

            var isCurrentlyBlocked = user.IsBlocked && (user.BlockedUntil == null || user.BlockedUntil > DateTime.UtcNow);

            if (isCurrentlyBlocked)
                throw new BusinessRuleException("User is already blocked");

            // =========================
            // Apply block
            // =========================

            user.IsBlocked = true;
            user.BlockedUntil = command.BlockedUntil;
            user.BlockReason = command.Reason;

            user.BlockedByAdminId = _currentUser.UserId;

            user.UpdatedAt = DateTime.UtcNow;

            // =========================
            // Save
            // =========================

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
