using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using System.Diagnostics;
using InventoryManagementSystem.Repository;

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
                bool U = await signupRepository.AddUser(user);
                if (U)
                {
                    if (user.UserType == "Admin")
                    {
                        // Redirect to admin dashboard
                        return RedirectToAction("AdminView");
                    }
                    else if (user.UserType == "Employee")
                    {
                        // Redirect to user dashboard
                        return RedirectToAction("EmployeeView");
                    }
                    else
                    {
                        // Invalid user type
                        return RedirectToAction("SignUp");
                    }
                }
                else
                {
                    // Invalid credentials
                    return RedirectToAction("SignUp");
                }
            }
            return View();
        }

        

        public async Task<IActionResult> Index(Login login)
        {
            if (ModelState.IsValid)
            {
                bool U = await signupRepository.LoginUser(login);
                if (U)
                {
                    if (login.UserType == "Admin")
                    {
                        // Redirect to admin dashboard
                        return RedirectToAction("ProductView");
                    }
                    else if (login.UserType == "Employee")
                    {
                        // Redirect to user dashboard
                        return RedirectToAction("EmployeeView");
                    }
                    else
                    {
                        // Invalid user type
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Invalid credentials
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }
            return View();
        }

        public IActionResult EmployeeView()
        {
            

            return View();
        }

        public IActionResult ProductView()
        {
            

            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                bool U = await productRepository.CreateProduct(product);
                return RedirectToAction("ProductView");
            }

            return RedirectToAction("AddProduct");
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

