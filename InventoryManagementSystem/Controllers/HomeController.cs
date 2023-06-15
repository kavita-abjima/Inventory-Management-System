using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using System.Diagnostics;
using InventoryManagementSystem.Repository;
using System.Data.SqlClient;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignUpRepository signupRepository;
        private readonly IProductRepository productRepository;

        public HomeController(ILogger<HomeController> logger, ISignUpRepository signupRepository, IProductRepository productRepository)
        {
            _logger = logger;
            this.signupRepository = signupRepository;
            this.productRepository = productRepository;
            

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
                    bool U = await signupRepository.AddUser(user);
                    if (U)
                    {
                        if (user.UserType == "Admin")
                        {
                            return RedirectToAction("ProductView");
                        }
                        else if (user.UserType == "Employee")
                        {
                            return RedirectToAction("EmployeeView");
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
                    // Handle the custom exception thrown by the stored procedure
                   
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View();
                    
                }
            }
            return View();
        }

        //public async Task<IActionResult> SignUp(Users user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool U = await signupRepository.AddUser(user);
        //        if (U)
        //        {
        //            if (user.UserType == "Admin")
        //            {
        //                // Redirect to admin dashboard
        //                return RedirectToAction("ProductView");
        //            }
        //            else if (user.UserType == "Employee")
        //            {
        //                return RedirectToAction("EmployeeView");
        //            }
        //            else
        //            {
        //                return RedirectToAction("SignUp");
        //            }
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    return View();
        //}

        public async Task<IActionResult> Index(Login login)
        {
            if (ModelState.IsValid)
            {
                bool U = await signupRepository.LoginUser(login);
                if (U)
                {
                    if (login.UserType == "Admin")
                    {
                        return RedirectToAction("ProductView");
                    }
                    else if (login.UserType == "Employee")
                    {
                        return RedirectToAction("EmployeeView");
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
            return View();
        }

        public IActionResult EmployeeView()
        {   
            return View();
        }

        //public IActionResult ProductView()
        //{      
        //    return View();
        //}

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
        public async Task<IActionResult> ProductView()
        {
            List<Product> products = await productRepository.GetAllProduct();
            return View(products);
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

