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
    public class CartsController : Controller
    {
        private ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        // GET: Carts
        public async Task<ActionResult> Index()
        {
            var carts = await _cartRepository.GetAllCartsAsync();
            return View(carts);
        }

        // GET: Carts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = await _cartRepository.GetCartAsync(id.Value);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUser")] Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return View(cart);
            }

            var result = await _cartRepository.SaveCartAsync(cart);
            if (!result)
                return View("Error");

            var carts = await _cartRepository.GetAllCartsAsync();
            return PartialView("_cartListPartial", carts);
        }

        // GET: Carts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = await _cartRepository?.GetCartAsync(id.Value);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUser")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                await _cartRepository.SaveCartAsync(cart);
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = await _cartRepository.GetCartAsync(id.Value);

            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            await _cartRepository.DeleteCartAsync(cart.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CartListPartial()
        {
            var carts = Task.Run(() => _cartRepository.GetAllCartsAsync()).Result;
            return PartialView("_cartListPartial", carts);
        }
    }
}
