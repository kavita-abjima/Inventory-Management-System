﻿using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;
using InventoryManagementSystem.Infrastructure;

namespace InventoryManagementSystem.Repository
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly DapperContext _context;

        public SignUpRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(Users user)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username);
                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.Email);
                parameters.Add("@UserType", user.UserType);
                parameters.Add("@SignupSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("SignupUser", parameters, commandType: CommandType.StoredProcedure);

                var signupSuccess = parameters.Get<bool>("@SignupSuccess");

                return signupSuccess;
            }
        }
        public async Task<bool> LoginUser(Login login)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", login.Username);
                parameters.Add("@Password", login.Password);
                parameters.Add("@UserType", login.UserType);
                parameters.Add("@LoginSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("LoginProcedure", parameters, commandType: CommandType.StoredProcedure);
                bool loginSuccess = parameters.Get<bool>("@LoginSuccess");

                return loginSuccess;
            }
        }
       
        public async Task<bool> CheckUserExists(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@UserExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("CheckUserExists", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<bool>("@UserExists");
            }
        }

    }
}
