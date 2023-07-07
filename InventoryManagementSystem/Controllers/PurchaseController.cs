using InventoryManagementSystem.Infrastructure;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace InventoryManagementSystem.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseController(IProductRepository productRepository, IPurchaseRepository purchaseRepository)
        {
            _productRepository = productRepository;
            _purchaseRepository = purchaseRepository;
        }


        public async Task<IActionResult> DisplayPurchase()
        {
            if (HttpContext.Session.GetString("UserType") != "Employee")
            {
                return RedirectToAction("Index","Home");
            }
            List<Purchase> purchase = await _purchaseRepository.GetAllPurchase();
            return View(purchase);
        }

        //Select Items for purchase
        [HttpGet]
        public async Task<IActionResult> PurchaseProduct()
        {
            var productList = await _productRepository.GetAvailableProductNames();
            var selectList = new SelectList(productList);

            TempData["Purchase_product"] = selectList;

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> PurchaseProduct(Purchase model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _purchaseRepository.AddProductToPurchase(model.Purchase_product, model.Purchase_quantity, model.PurchaseDate, model.PurchaseBy);

                    if (result == "Success")
                    {
                        return RedirectToAction("DisplayPurchase", "Purchase");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = result;
                        return RedirectToAction("PurchaseProduct");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var productList = await _productRepository.GetAvailableProductNames();
            var selectList = new SelectList(productList);

            TempData["Purchase_product"] = selectList;

            return View(model);
        }



        //public async Task<IActionResult> UpdatePurchaseProduct(int purchaseId)
        //{
        //    var existingProduct = await _purchaseRepository.GetPurchaseProductById(purchaseId);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    var productList = await _productRepository.GetAvailableProductNames();
        //    ViewBag.ProductList = new SelectList(productList);

        //    return View(existingProduct);
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdatePurchaseProduct(Purchase updatedProduct)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       _purchaseRepository.UpdatePurchaseProduct(updatedProduct);

        //        return RedirectToAction("DisplayPurchase", new { purchaseId = updatedProduct.PurchaseId});
        //    }

        //    return View(updatedProduct);
        //}

        //Detail of purchased product
        public async Task<IActionResult> PurchaseProductDetail(int purchaseId)
        {
            Purchase existingProduct = await _purchaseRepository.GetPurchaseProductById(purchaseId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            return View(existingProduct);
        }
        //Delete purchased product
        public IActionResult Delete(int purchaseId)
        {
            _purchaseRepository.DeletePurchase( purchaseId);
            return RedirectToAction("DisplayPurchase");
        }

    }
}
