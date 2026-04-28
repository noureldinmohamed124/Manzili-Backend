using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Services.CreateService;
using Manzili.Domain.Entities;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class CreateServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceUseCase(IServiceRepo serviceRepo, ICategoryRepo categoryRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _serviceRepo = serviceRepo;
            _categoryRepo = categoryRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateServiceCommand command)
        {
            int sellerId = _currentUser.UserId;

            await ValidateAsync(command);

            var service = new Service
            {
                Title = command.Title,
                ServiceDescription = command.Description,
                CategoryId = command.CategoryId,
                BasePrice = command.BasePrice,
                ProviderId = sellerId,
                StatusId = (int)ServiceStatus.Active
            };

            foreach (var image in command.Images)
            {
                service.ServiceImages.Add(
                    new ServiceImage{
                        ImageUrl = image
                    }
                );
            }

            foreach (var group in command.OptionGroups)
            {
                var optionGroup = new ServiceOptionGroup
                {
                    Name = group.Name,
                    IsRequired = group.IsRequired
                };

                foreach (var option in group.Options)
                {
                    optionGroup.Options.Add(new ServiceOption
                    {
                        ServiceOptionName = option.Name,
                        PriceAdjustment = option.Price
                    });
                }

                service.OptionGroups.Add(optionGroup);
            }

            await _serviceRepo.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ValidateAsync(CreateServiceCommand command)
        {
            if (string.IsNullOrWhiteSpace(
                command.Title))
            {
                throw new ValidationException("Title is required");
            }

            if (command.BasePrice < 0)
            {
                throw new ValidationException("Invalid base price");
            }

            if (!command.Images.Any())
            {
                throw new ValidationException("At least one image is required");
            }

            //var categoryExists = await _categoryRepo.ExistsAsync(command.CategoryId);
            //if (!categoryExists)
            //{
            //    throw new NotFoundException(
            //        "Category not found");
            //}

            /*
             DO NOT trust frontend validation.

                Always validate:
                
                category existence
                prices
                ownership
                required collections
                
                backend-side.
             */
        }
    }
}
