using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ordersProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ordersProject.Context;

namespace ordersProject.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public IEnumerable<Order> ViewPage()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Orders.ToList();
            }
        }
        public IEnumerable<Order> LoadPage()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Orders.ToList();
            }
        }
        [HttpPost]
        public IActionResult Filter([FromBody]int id)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var list = context.Orders.Where(u => id == u.Oxid).ToList();
                    return Ok(list);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult AddOrders([FromBody] List<Order> orders)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var order in orders)
                        context.Orders.Add(order);
                    context.SaveChanges();
                }
                return Ok(orders);
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPatch]
        public IActionResult UpdateOrders([FromBody]List<UpdateObject> objectsToUpdate)
        {
            try
            {
                var ordersNeedToUpdateByStatus = objectsToUpdate.Where(u => u.Type == "Statuses").SelectMany(u => u.KeyValuePairs.Keys).ToList();
                var ordersNeedToUpdateByInvoiceNumber = objectsToUpdate.Where(u => u.Type == "InvoiceNumber").SelectMany(u => u.KeyValuePairs.Keys).ToList();
                if (ordersNeedToUpdateByInvoiceNumber.Count != 0 && ordersNeedToUpdateByStatus.Count != 0)
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        if (ordersNeedToUpdateByStatus.Count != 0)
                        {
                            var ordersToUpdateByStatus = context.Orders.Where(u => ordersNeedToUpdateByStatus.Contains(u.Oxid)).ToList();
                            foreach (var order in ordersToUpdateByStatus)
                                order.Status = objectsToUpdate.FirstOrDefault(u => u.Type == "Statuses").KeyValuePairs[order.Oxid];

                        }
                        if (ordersNeedToUpdateByInvoiceNumber.Count != 0)
                        {
                            var ordersToUpdateByStatus = context.Orders.Where(u => ordersNeedToUpdateByInvoiceNumber.Contains(u.Oxid)).ToList();
                            foreach (var order in ordersToUpdateByStatus)
                                order.InvoiceNumber = int.Parse(objectsToUpdate.FirstOrDefault(u => u.Type == "InvoiceNumber").KeyValuePairs[order.Oxid]);
                        }
                        context.SaveChanges();
                    }
                return Ok(objectsToUpdate);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}
