using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TNAI.Model;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;
using TNAI.Repository.Concrete;

namespace TNAI.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreationDate")] Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            var result = await _orderRepository.SaveOrderAsync(order);
            if (!result)
                return View("Error");

            var orders = await _orderRepository.GetAllOrdersAsync();
            return PartialView("_orderListPartial", orders);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreationDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.SaveOrderAsync(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            await _orderRepository.DeleteOrderAsync(order.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult OrderListPartial()
        {
            var orders = Task.Run(() => _orderRepository.GetAllOrdersAsync()).Result;
            return PartialView("_orderListPartial", orders);
        }

    }
}
