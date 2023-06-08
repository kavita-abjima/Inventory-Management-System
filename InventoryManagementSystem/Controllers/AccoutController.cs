using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AccoutController : Controller
    {
        private readonly ISignUpRepository signupRepository;

        public AccoutController(ISignUpRepository signupRepository)
        {
            this.signupRepository = signupRepository;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Users user)
        {
            if (ModelState.IsValid)
            {
                
                signupRepository.AddUser(user);

               
                return RedirectToAction("SignupSuccess");
            }

            // If the model state is not valid, return the signup view with validation errors
            return View(user);
        }

        public IActionResult SignupSuccess()
        {
            return View();
        }
    }
}
