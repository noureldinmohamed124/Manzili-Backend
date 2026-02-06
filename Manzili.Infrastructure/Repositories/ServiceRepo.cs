using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Queries.Services.GetPaginatedServices;
using Manzili.Application.Queries.Services.GetServiceDetails;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class ServiceRepo : GenericRepo<Service>, IServiceRepo
    {
        public ServiceRepo(ManziliDbContext context) : base(context) { }

        public async Task<PaginatedServiceListDto> GetAllPaginatedForListingAsync(GetServicesQuery q)
        {
            var query = _context.Services
                .Where(s => s.Status.IsActive).AsQueryable();

            if (q.CategoryId.HasValue)
                query = query.Where(s => s.CategoryId == q.CategoryId);

            if (q.IsRecommended.HasValue)
                query = query.Where(s => s.IsRecommended == q.IsRecommended);

            if (q.IsFeatured.HasValue)
                query = query.Where(s => s.IsFeatured == q.IsFeatured);

            var totalServices = await query.CountAsync();

            var skipedServices = ((q.Page - 1) * q.PageSize);
            var services = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip(skipedServices)
                .Take(q.PageSize)
                .Select(s => new ServiceListItemDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    BasePrice = s.BasePrice,
                    ProviderName = s.Provider.FullName,
                    Rating = 0,
                    ImageUrl = s.ServiceImages.Select(si => si.ImageUrl).FirstOrDefault()
                })
                .AsNoTracking()
                .ToListAsync();

            var pag = new PaginatedServiceListDto
            {
                Items = services,
                Page = q.Page,
                PageSize = q.PageSize,
                TotalPages = (int)Math.Ceiling((decimal)totalServices / q.PageSize)
            };
            return pag;

        }

        public async Task<ServiceDetailsDto?> GetServiceDetailsByIdAsync(int Id)
        {
            return await _context.Services
                .Where(s => s.Id == Id)
                .Select(ss => new ServiceDetailsDto
                {
                    Id = ss.Id,
                    Title = ss.Title,
                    ServiceDescription = ss.ServiceDescription,
                    //Price = ss.Price,
                    Address = "Default Address",
                    Provider = new Provider
                    {
                        Id = ss.Provider.Id,
                        FullName = ss.Provider.FullName,
                        rating = 5, // defualt value right now
                        ReviewsNo = 0 // defualt value right now
                    },
                    Options = ss.ServiceOptions.Select(o => new OptionsList
                    {
                        Id = o.Id,
                        ServiceOptionName = o.ServiceOptionName,
                        Price = o.Price,
                    }).ToList(),
                    Images = ss.ServiceImages.Select(i => new Image
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
