using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Services.UpdateService;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class UpdateServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceUseCase(IServiceRepo serviceRepo, ICategoryRepo categoryRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _serviceRepo = serviceRepo;
            _categoryRepo = categoryRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        //public async Task ExecuteAsync(UpdateServiceCommand command)
        //{
        //    var sellerId = _currentUser.UserId;

        //    // 1. Get service
        //    var service = await _serviceRepo.GetServiceDetailsByIdAsync(command.ServiceId);

        //    if (service == null)
        //        throw new NotFoundException("Service not found");

        //    // 2. Ownership check
        //    if (service.Provider.Id != sellerId)
        //        throw new ForbiddenException("You cannot edit this service");

        //    // 3. Validate category
        //    var categoryExists = await _categoryRepo.ExistsAsync(command.CategoryId);
        //    if (!categoryExists)
        //        throw new NotFoundException("Category not found");

        //    // 4. Update basic fields
        //    service.Title = command.Title;
        //    service.ServiceDescription = command.Description;
        //    service. = command.CategoryId;
        //    service.BasePrice = command.BasePrice;

        //    // 5. Replace Images
        //    service.ServiceImages.Clear();

        //    foreach (var img in command.Images)
        //    {
        //        service.ServiceImages.Add(new ServiceImage
        //        {
        //            ImageUrl = img
        //        });
        //    }

        //    // 6. Replace Option Groups
        //    service.ServiceOptionGroups.Clear();

        //    foreach (var group in command.OptionGroups)
        //    {
        //        var optionGroup = new ServiceOptionGroup
        //        {
        //            GroupName = group.Name,
        //            IsRequired = group.IsRequired
        //        };

        //        foreach (var option in group.Options)
        //        {
        //            optionGroup.ServiceOptions.Add(new ServiceOption
        //            {
        //                ServiceOptionName = option.Name,
        //                PriceAdjustment = option.Price
        //            });
        //        }

        //        service.ServiceOptionGroups.Add(optionGroup);
        //    }

        //    // 7. Save
        //    await _unitOfWork.SaveChangesAsync();
        //}

    }
}
