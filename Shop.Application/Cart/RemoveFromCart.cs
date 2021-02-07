using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public RemoveFromCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public bool All { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
            {
                return true;
            }
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == request.StockId))
            {
                return true;
            }

            var stockOnHold = _ctx.StocksOnHold.FirstOrDefault(x => x.StockId == request.StockId && x.SessionId == _session.Id);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == request.StockId);
            if (request.All)
            {
                stock.Qty += stockOnHold.Qty;
                stockOnHold.Qty = 0;
                cartList.Find(x => x.StockId == request.StockId).Qty = 0;
            }
            else
            {
                stock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;
                cartList.Find(x => x.StockId == request.StockId).Qty -= request.Qty;
            }

            if (stockOnHold.Qty <= 0)
            {
                _ctx.Remove(stockOnHold);
            }
            stringObject = JsonConvert.SerializeObject(cartList.Where(x => x.Qty != 0));

            _session.SetString("cart", stringObject);
            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}
