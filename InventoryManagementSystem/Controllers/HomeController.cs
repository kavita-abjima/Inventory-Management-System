using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis;
using InventoryManagementSystem.Infrastructure;
using InventoryManagementSystem.Repository;
using NuGet.Protocol.Core.Types;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISignUpRepository signupRepository;
        private readonly IProductRepository productRepository;
        

        public HomeController(ISignUpRepository signupRepository, IProductRepository productRepository)
        {
            this.signupRepository = signupRepository;
            this.productRepository = productRepository;
            
        }

        public IActionResult SignUp()
        {
            return View();
        }

        // SignUp
        [HttpPost]
        public async Task<IActionResult> SignUp(Users user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool userExists = await signupRepository.CheckUserExists(user.Email);
                    if (userExists)
                    {
                        ModelState.AddModelError(string.Empty, "User already exists with the provided email address.");
                        return View();
                    }

                    bool signupSuccess = await signupRepository.AddUser(user);
                    if (signupSuccess)
                    {
                        if (user.UserType == "Admin")
                        {
                            TempData["SuccessMessage"] = "Sign-Up successful!";
                            return RedirectToAction("ProductView");
                        }
                        else if (user.UserType == "Employee")
                        {
                            TempData["SuccessMessage"] = "Sign-Up successful!";
                            return RedirectToAction("DisplayPurchase", "Purchase");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View();
                }
            }

            return View();
        }

        // Index (Login)
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUserLoggedIn = await signupRepository.LoginUser(login);
                    if (isUserLoggedIn)
                    {
                        if (login.UserType == "Admin")
                        {
                            TempData["SuccessMessage"] = "Login-in successful!";
                            HttpContext.Session.SetString("UserType", "Admin");
                            return RedirectToAction("ProductView");
                        }
                        else if (login.UserType == "Employee")
                        {
                            TempData["SuccessMessage"] = "Login-in successful!";
                            HttpContext.Session.SetString("UserType", "Employee");
                            return RedirectToAction("DisplayPurchase", "Purchase");
                        }
                    }
                }

                return View(login);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(login);
            }
        }
        //Logout
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // AddProduct
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isCreated = await productRepository.CreateProduct(product);
                    if (isCreated)
                    {
                        return RedirectToAction("ProductView");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        // ProductView
        public async Task<IActionResult> ProductView()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
            {
                return RedirectToAction("Index");
            }
            List<Product> products = await productRepository.GetAllProduct();
            return View(products);
        }

        // UpdateProduct
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var existingProduct = await productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            return View(existingProduct);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                await productRepository.EditProduct(updatedProduct);

                return RedirectToAction("ProductView", new { id = updatedProduct.ProductId });
            }

            return View(updatedProduct);
        }

        // ProductDetails
        public async Task<IActionResult> ProductDetails(int id)
        {
            Product existingProduct = await productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            return View(existingProduct);
        }
        public IActionResult Delete(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("ProductView");
        }

        public async Task<IActionResult> ProductReport()
        {
            var productList = await productRepository.GetAllProducts();
            return View(productList);
        }

        [HttpPost]
        public async Task<IActionResult> ProductReport(DateTime startDate, DateTime endDate)
        {
            var productList = await productRepository.GetAllProductsAsync(startDate, endDate);
            return View(productList);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


