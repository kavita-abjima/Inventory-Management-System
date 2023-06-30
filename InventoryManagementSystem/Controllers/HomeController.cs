using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis;
using InventoryManagementSystem.Infrastructure;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignUpRepository signupRepository;
        private readonly IProductRepository productRepository;
        private readonly IPurchaseRepository purchaseRepository;

        public HomeController(ILogger<HomeController> logger, ISignUpRepository signupRepository, IProductRepository productRepository, IPurchaseRepository purchaseRepository)
        {
            _logger = logger;
            this.signupRepository = signupRepository;
            this.productRepository = productRepository;
            this.purchaseRepository = purchaseRepository;           
        }

        public IActionResult SignUp()
        {
            return View();
        }

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
                            return RedirectToAction("ProductView");
                        }
                        else if (user.UserType == "Employee")
                        {
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

        //Login
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
                            HttpContext.Session.SetString("UserType", "Admin");
                            return RedirectToAction("ProductView");
                        }
                        else if (login.UserType == "Employee")
                        {
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

        //purchase

        public ActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await productRepository.CreateProduct(product);
                if (isCreated)
                {
                    return RedirectToAction("ProductView"); 
                }
            }

            return View(); 
        }


        //ProductDetails View
        public async Task<IActionResult> ProductView()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
            {
                return RedirectToAction("Index");
            }
            List<Product> products = await productRepository.GetAllProduct();
            return View(products);
        }

        //Update the product
        public IActionResult UpdateProduct(int id)
        {
            var existingProduct = productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            return View(existingProduct);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                productRepository.EditProduct(updatedProduct);

                return RedirectToAction("ProductView", new { id = updatedProduct.ProductId });
            }

            return View(updatedProduct);
        }
        //Details button
        public IActionResult ProductDetails(int id)
        {
            Product existingProduct = productRepository.GetProductById(id);

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

