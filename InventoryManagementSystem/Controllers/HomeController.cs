using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using System.Diagnostics;
using InventoryManagementSystem.Repository;

namespace  InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignUpRepository signupRepository;

        public HomeController(ILogger<HomeController> logger, ISignUpRepository signupRepository)
        {
            _logger = logger;
            this.signupRepository = signupRepository;
        }

        public IActionResult Index()
        {
            return View();
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
                        return RedirectToAction("SignupSuccess");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                
            }

            // If the model state is not valid, return the signup view with validation errors
            return View(user);
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

