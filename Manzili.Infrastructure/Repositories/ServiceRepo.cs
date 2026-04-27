using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Queries.Services.GetPaginatedServices;
using Manzili.Application.Queries.Services.GetServiceByName;
using Manzili.Application.Queries.Services.GetServiceDetails;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Data.QueryExtensions;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class ServiceRepo : GenericRepo<Service>, IServiceRepo
    {
        public ServiceRepo(ManziliDbContext context) : base(context) { }

        public async Task<HomeServicesDto> GetHomeServicesAsync(int take = 10)
        {
            var baseQuery = _context.Services
                .Where(s => s.Status.IsActive)
                .AsNoTracking();

            // Top Discounts
            var topDiscountsTask = await baseQuery
                .Where(s => s.HasActivePromotion)
                .OrderByDescending(s => s.CreatedAt)
                .Take(take)
                .Select(ToListItem())
                .ToListAsync();

            // Recommended
            var recommendedTask = await baseQuery
                .Where(s => s.IsRecommended)
                .OrderByDescending(s => s.CreatedAt)
                .Take(take)
                .Select(ToListItem())
                .ToListAsync();

            // Most Purchased
            var mostPurchasedTask = await baseQuery
                .OrderByDescending(s => s.TotalPurchases)
                .Take(take)
                .Select(ToListItem())
                .ToListAsync();

            // Regular (Latest Services)
            var regularTask = await baseQuery
                .OrderByDescending(s => s.CreatedAt)
                .Take(take)
                .Select(ToListItem())
                .ToListAsync();

            // Execute in parallel
            //await Task.WhenAll(
            //    topDiscountsTask,
            //    recommendedTask,
            //    mostPurchasedTask,
            //    regularTask
            //);

            return new HomeServicesDto
            {
                TopDiscounts = topDiscountsTask,
                Recommended = recommendedTask,
                MostPurchased = mostPurchasedTask,
                Regular = regularTask
            };
        }

        public async Task<PaginatedServiceListDto> GetAllPaginatedForListingAsync(GetServicesQuery q)
        {
            var baseQuery = _context.Services
                .Where(s => s.Status.IsActive && s.Provider.IsBlocked == false)
                .AsNoTracking();

            var query = new ServiceQueryBuilder(baseQuery)
                .FilterByCategory(q.CategoryId)
                .ApplyFilter(q.Filter)
                .ApplySorting(q.SortBy)
                .Build();

            int skip = (q.Page - 1) * q.PageSize;

            var services = await query
                .Skip(skip)
                .Take(q.PageSize + 1) // fetch extra row
                .Select(ToListItem())
                .ToListAsync();

            bool hasMore = services.Count > q.PageSize;

            if (hasMore)
                services.RemoveAt(q.PageSize);

            return new PaginatedServiceListDto
            {
                Items = services,
                Page = q.Page,
                PageSize = q.PageSize,
                HasMore = hasMore
            };
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
                    BasePrice = ss.BasePrice,
                    Address = "Default Address",

                    Provider = new Provider
                    {
                        Id = ss.Provider.Id,
                        FullName = ss.Provider.FullName,
                        rating = 5, // defualt value right now
                        ReviewsNo = 0 // defualt value right now
                    },

                    OptionGroups = ss.OptionGroups
                        .OrderBy(og => og.DisplayOrder)
                        .Select(g => new OptionGroupDto
                        {
                            Id = g.Id,
                            Name = g.Name,
                            IsRequired = g.IsRequired,
                            AllowMultiple = g.AllowMultiple,
                            
                            Options = g.Options
                                .OrderBy(o => o.DisplayOrder)
                                .Select(o => new OptionsListDto{
                                    Id = o.Id,
                                    ServiceOptionName = o.ServiceOptionName,
                                    PriceAdjustment = o.PriceAdjustment
                                })
                                .ToList()
                        })
                        .ToList(),
                    
                    Images = ss.ServiceImages.Select(i => new ImageDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<ServiceSearchDto>> SearchByNameAsync(SearchServicesQuery query)
        {
            var baseQuery = _context.Services
                .AsNoTracking()
                .Where(s => s.Title.Contains(query.Keyword));

            var totalCount = await baseQuery.CountAsync();

            var services = await baseQuery
                .OrderBy(s => s.Id)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(s => new ServiceSearchDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    BasePrice = s.BasePrice,
                    ProviderName = s.Provider.FullName,
                    ThumbnailImageUrl = s.ServiceImages
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return new PagedResult<ServiceSearchDto>
            {
                Items = services,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        // Helper - Mapper
        public static Expression<Func<Service, ServiceListItemDto>> ToListItem()
        {
            return s => new ServiceListItemDto
            {
                Id = s.Id,
                Title = s.Title,
                BasePrice = s.BasePrice,
                ProviderName = s.Provider.FullName,
                Rating = 0,
                ImageUrl = s.ServiceImages
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault(),
                CreatedAtDate = DateOnly.FromDateTime(s.CreatedAt),
            };
        }

        
    }
}
