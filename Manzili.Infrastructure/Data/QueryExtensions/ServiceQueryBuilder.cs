using Manzili.Application.Common.Enums;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Data.QueryExtensions
{
    public class ServiceQueryBuilder
    {
        private IQueryable<Service> _query;

        public ServiceQueryBuilder(IQueryable<Service> query)
        {
            _query = query;
        }

        public ServiceQueryBuilder FilterByCategory(int? categoryId)
        {
            if (categoryId.HasValue)
                _query = _query.Where(s => s.CategoryId == categoryId);

            return this;
        }

        public ServiceQueryBuilder ApplyFilter(ServiceFilterType? filter)
        {
            if (!filter.HasValue)
                return this;

            _query = filter switch
            {
                ServiceFilterType.Recommended =>
                    _query.Where(s => s.IsRecommended),

                ServiceFilterType.TopDiscounts =>
                    _query.Where(s => s.HasActivePromotion),

                ServiceFilterType.MostPurchased =>
                    _query.OrderByDescending(s => s.TotalPurchases),

                _ => _query
            };

            return this;
        }

        public ServiceQueryBuilder ApplySorting(ServiceSortBy? sortBy)
        {
            if (!sortBy.HasValue)
            {
                _query = _query.OrderByDescending(s => s.CreatedAt);
                return this;
            }

            _query = sortBy switch
            {
                ServiceSortBy.PriceAsc =>
                    _query.OrderBy(s => s.BasePrice),

                ServiceSortBy.PriceDesc =>
                    _query.OrderByDescending(s => s.BasePrice),

                ServiceSortBy.MostPurchased =>
                    _query.OrderByDescending(s => s.TotalPurchases),

                _ =>
                    _query.OrderByDescending(s => s.CreatedAt)
            };

            return this;
        }

        public IQueryable<Service> Build()
        {
            return _query;
        }

    }
}
