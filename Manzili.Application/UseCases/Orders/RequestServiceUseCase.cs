using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Commands.Orders;
using Manzili.Application.Exceptions;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Orders
{
    public class RequestServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUserService;

        public RequestServiceUseCase(IServiceRepo serviceRepo, IOrderRepo orderRepo)
        {
            _serviceRepo = serviceRepo;
            _orderRepo = orderRepo;
        }

        public async Task ExecuteAsync(RequestServiceCommand command)
        {
            int userId = _currentUserService.UserId;

            var service = await _serviceRepo.GetByIdAsync(command.ServiceId);

            if (service == null)
                throw new NotFoundException("Service not found");

            var transaction = new Transaction
            {

            };

            foreach(var optionGroup in command.OptionGroups)
            {
                foreach (var option in optionGroup.Options)
                {

                }
            }


        }
    }
}
