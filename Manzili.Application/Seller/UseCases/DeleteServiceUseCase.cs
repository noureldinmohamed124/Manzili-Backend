using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Services.DeleteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class DeleteServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteServiceUseCase(IServiceRepo serviceRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _serviceRepo = serviceRepo;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task ExecuteAsync(DeleteServiceCommand command)
        {
            int sellerId = _currentUser.UserId;

            var service = await _serviceRepo.GetServiceByIdWithTransactionsAsync(command.ServiceId);

            if (service == null || service.IsDeleted)
            {
                throw new NotFoundException("Service not found");
            }

            if (service.ProviderId != sellerId)
            {
                throw new ForbiddenException("You cannot delete this service");
            }

            bool hasActiveOrders = service.Transactions
                .Any(t => !( (OrderTransactionTypeEnum)t.TransactionTypeId ).IsTerminal());

            if (hasActiveOrders)
            {
                throw new ConflictException("Cannot delete service with active orders");
            }

            service.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
