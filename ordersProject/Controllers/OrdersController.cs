using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ordersProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ordersProject.Context;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace ordersProject.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public IActionResult ViewPage()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var orders = context.Orders.Include(o => o.Articles).Include(o => o.BillingAddress).ThenInclude(o => o.Country).Include(o => o.Payments).ToList();
                    return View(orders);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult LoadPage()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var orders = context.Orders.Include(o => o.Articles).Include(o => o.BillingAddress).ThenInclude(o => o.Country).Include(o => o.Payments).ToList();
                    return View(orders);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult ViewPage(long id)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var orders = context.Orders.Include(o => o.Articles).Include(o => o.BillingAddress).ThenInclude(o => o.Country).Include(o => o.Payments).Where(o => EF.Functions.Like(o.Oxid.ToString(), $"%{id.ToString()}%")).ToList();
                    return View(orders);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AddOrders(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file.");

                var fileText = new StreamReader(file.OpenReadStream()).ReadToEnd();
                XDocument document = XDocument.Parse(fileText);

                List<Order> orders = new List<Order>();
                foreach (var element in document.Element("orders").Elements("order"))
                    orders.Add(new Order(element));

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var order in orders)
                        context.Orders.Add(order);
                    context.SaveChanges();
                }
                return RedirectToAction("LoadPage");
            }
            catch (Exception exp)
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
