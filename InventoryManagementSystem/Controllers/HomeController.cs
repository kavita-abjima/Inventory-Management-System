﻿using Dapper;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(_config.GetConnectionString("conn"));
        //    }
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Users user)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("conn"));
            {
                connection.Execute("SignUp",user, commandType: CommandType.StoredProcedure);
                return View();

            }
            //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //public IActionResult Error()
            //{
            //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
    }

}










    