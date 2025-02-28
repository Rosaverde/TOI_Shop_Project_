﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Application.OrdersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]

    public class OrdersController : Controller
    {
        [HttpGet("")]
        public IActionResult GetOrders(int status, [FromServices] GetOrders getOrders) => Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder getOrder) => Ok(getOrder.Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromServices] UpdateOrder updateOrder) => Ok(await updateOrder.DoAsync(id));
    }
}
