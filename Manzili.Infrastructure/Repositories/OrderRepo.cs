using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Buyer.Queries.Orders.GetAllOrders;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Seller.Queries.Orders.GetAllSellerOrders;
using Manzili.Application.Seller.Queries.Orders.GetSellerOrderDetailsById;
using Manzili.Domain.Entities;
using Manzili.Domain.Enums;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class OrderRepo : GenericRepo<Transaction>, IOrderRepo
    {
        public OrderRepo(ManziliDbContext context) : base(context)
        {
        }

        public async Task<OrdersListDto> GetAllOrdersAsync(int buyerId, OrderTransactionTypeEnum? status)
        {
            var query = _context.Transactions
                .Where(t => t.BuyerId == buyerId);

            if (status.HasValue)
            {
                query = query.Where(t => t.TransactionTypeId == (int)status.Value);
            }

            var orders = new OrdersListDto();


            orders.Items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new OrderItemDto
                {
                    Id = t.Id,
                    OrderCode = t.TransactionCode,
                    ServiceName = t.Service!.Title,
                    RawOrderPrice = t.TotalPrice,
                    DeliveryFees = t.DeliveryFees,
                    TotalPrice = t.TotalPrice,
                    Status = t.TransactionType.TransactionTypeName,
                    ProviderName = t.Provider.FullName,
                    CreatedAt = t.CreatedAt,
                    CustomizationDetails = t.CustomRequestText,
                    Options = t.TransactionOptions.Select(to => new OrderItemOptions
                    {
                        GroupOption = to.ServiceOptionGroup.Name,
                        Option = to.OptionName,
                        Quantity = to.Quantity,
                    }).ToList()
                }).ToListAsync();

            return orders;
        }

        public async Task<Transaction?> GetOrderByCode(string transactionCode)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionCode == transactionCode);
        }

        // for Submit Payment
        public async Task<List<Transaction>> GetOrdersForPaymentAsync(int buyerId, IReadOnlyList<int> orderIds)
        {
            return await _context.Transactions
                .Where(t => t.BuyerId == buyerId && orderIds.Contains(t.Id))
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetOrdersForPaymentSummaryAsync(int buyerId, IReadOnlyList<int> orderIds)
        {
            return await _context.Transactions
                .Include(t => t.Service).ThenInclude(s => s!.ServiceImages)
                .Include(t => t.TransactionOptions)
                .Where(t => t.BuyerId == buyerId && orderIds.Contains(t.Id))
                .ToListAsync();
        }

 
        // For get Seller Orders
        public async Task<SellerOrdersListDto> GetSellerOrdersAsync(int sellerId, GetSellerOrdersQuery query)
        {
            var ordersQuery = _context.Transactions.AsNoTracking()
                .Where(t => t.ProviderId == sellerId && t.ServiceId != null);

            if (query.Status.HasValue)
            {
                ordersQuery = ordersQuery.Where(t => t.TransactionTypeId == (int)query.Status.Value);
            }

            int totalCount = await ordersQuery.CountAsync();

            int skip = (query.Page - 1) * query.PageSize;

            var orders = await ordersQuery
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(query.PageSize)
                .Select(t => new SellerOrderItemDto
                {
                    Id = t.Id,
                    OrderCode = t.TransactionCode,
                    ProviderName = t.Buyer.FullName,
                    ServiceTitle = t.Service!.Title,
                    TotalPrice = t.TotalPrice,
                    ProposedPrice = (OrderTransactionTypeEnum)t.TransactionTypeId == OrderTransactionTypeEnum.RePriced ? t.ProposedPrice : null,
                    Status = t.TransactionType.TransactionTypeName,
                    CreatedAt = t.CreatedAt,
                    ServiceImage = t.Service
                        .ServiceImages.Select(i => i.ImageUrl).FirstOrDefault()
                }).ToListAsync();

            var ordersList = new SellerOrdersListDto
            {
                Items = orders,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize,
            };

            return ordersList;
        }

        public async Task<SellerOrderDetailsDto?> GetSellerOrderByIdAsync(int sellerId, int orderId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(t =>
                    t.Id == orderId &&
                    t.ProviderId == sellerId)
                .Select(t => new SellerOrderDetailsDto
                {
                    Id = t.Id,
                    BuyerName = t.Buyer.FullName,
                    BuyerPhone = t.Buyer.PhoneNumber ?? "No Phone Number",
                    ServiceTitle = t.Service!.Title,
                    ServiceImage = t.Service
                        .ServiceImages
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault(),

                    CustomRequestText = t.CustomRequestText,
                    CustomRequestImage = t.CustomRequestImage,
                    RawPrice = t.RawPrice,
                    TotalPrice = t.TotalPrice,
                    ProposedPrice = (OrderTransactionTypeEnum)t.TransactionTypeId == OrderTransactionTypeEnum.RePriced ? t.ProposedPrice : null,
                    RePricingReason = (OrderTransactionTypeEnum)t.TransactionTypeId == OrderTransactionTypeEnum.RePriced ? t.RePricingReason : null,
                    Status = t.TransactionType.TransactionTypeName,
                    CreatedAt = t.CreatedAt,
                    Options = t.TransactionOptions
                        .Select(o => new SellerOrderOptionDto
                        {
                            OptionGroupName = o.ServiceOptionGroup.Name,
                            OptionName = o.OptionName,
                            Price = o.Price,
                            Quantity = o.Quantity
                        }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<Transaction?> GetSellerOrderForUpdateAsync(int sellerId, int orderId)
        {
            var order = await _context.Transactions
                .FirstOrDefaultAsync(t => t.ProviderId == sellerId && t.Id == orderId && t.ServiceId != null);

            return order;
        }
    }
}
