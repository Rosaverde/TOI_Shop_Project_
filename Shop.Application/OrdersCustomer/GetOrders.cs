using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Enums;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.OrdersCustomer
{
    public class GetOrders
    {
        private ApplicationDbContext _ctx;

        public GetOrders(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<OrdersViewModel> Do(string userId) => 
            _ctx.Orders.Where(x => x.UserId == userId)
            .Include(x => x.OrderStocks)
            .ThenInclude(x => x.Stock)
            .ThenInclude(x => x.Product)
            .Select(x => new OrdersViewModel
            {
            Id = x.Id,
            Status = x.Status,

            Products = x.OrderStocks.Select(y => new Product {
            Name = y.Stock.Product.Name,
            Description = y.Stock.Product.Description,
            Qty = y.Qty,
            StockDescription = y.Stock.Description,
            })
        }).ToList();

        public class OrdersViewModel
        {
            public int Id { get; set; }
            public OrderStatus Status { get; set; }

            public IEnumerable<Product> Products { get; set; }

        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int Qty { get; set; }

            public string StockDescription { get; set; }
        }
    }
}
