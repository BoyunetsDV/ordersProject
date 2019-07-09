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
using System.Diagnostics;

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

        [HttpPost]
        public IActionResult LoadPage(List<UpdateObject> objectsToUpdate)
        {
            try
            {
                var orders = new List<Order>();
                if (objectsToUpdate.Count != 0)
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        var objectIds = objectsToUpdate.Select(o => o.Id).ToList();
                        var ordersToUpdate = context.Orders.Where(u => objectIds.Contains(u.Id)).ToList();
                        foreach (var order in ordersToUpdate)
                        {
                            var updateData = objectsToUpdate.FirstOrDefault(o => o.Id == order.Id);
                            order.InvoiceNumber = updateData.InvoiceNumber;
                            order.Status = updateData.Status;
                        }

                        context.SaveChanges();

                        orders = context.Orders.Include(o => o.Articles).Include(o => o.BillingAddress).ThenInclude(o => o.Country).Include(o => o.Payments).ToList();
                    }
                return View(orders);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
