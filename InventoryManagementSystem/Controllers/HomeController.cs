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

        public HomeController(ILogger<HomeController> logger, ISignUpRepository signupRepository)
        {
            _logger = logger;
            this.signupRepository = signupRepository;
        }

        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
        //public async Task<IActionResult> Index(string username, string password, string userType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //            bool U = await signupRepository.LoginUser(username, password,userType);
        //            if (U)
        //            {
        //                return RedirectToAction("LoginSuccess");

        //            }
        //            else
        //            {
        //                return RedirectToAction("Index");
        //            }

        //    }
        //    return View();
        //}
        public async Task<IActionResult> Index(string username, string password, string userType)
        {
            if (ModelState.IsValid)
            {
                bool U = await signupRepository.LoginUser(username, password, userType);
                if (U)
                {
                    if (userType == "Admin")
                    {
                        // Redirect to admin dashboard
                        return RedirectToAction("AdminView");
                    }
                    else if (userType == "Employee")
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
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult EmployeeView()
        {
            

            return View();
        }

        public IActionResult AdminView()
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
                bool U = await signupRepository.AddUser(user);
                if (U)
                {
                    if (user.UserType == "Admin") 
                    {
                        // Redirect to admin dashboard
                        return RedirectToAction("AdminView");
                    }
                    else if (user.UserType== "Employee")
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




        //public async Task<IActionResult> SignUp(Users user)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        try
        //        {
        //            bool U = await signupRepository.AddUser(user);
        //            if (U)
        //            {
        //                return RedirectToAction("SignupSuccess");
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }


        //    }

        //    // If the model state is not valid, return the signup view with validation errors
        //    return View(user);
        //}

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

