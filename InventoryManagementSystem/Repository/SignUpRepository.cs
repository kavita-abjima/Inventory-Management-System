using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

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
                bool signupSuccess = parameters.Get<bool>("@SignupSuccess");



                return signupSuccess;

            }
        }
        //public async Task<bool> AddUser(Users user)
        //{
        //    try
        //    {
        //        using (var connection = _context.CreateConnection())
        //        {


        //            string storedProcedureName = "SignupUser";

        //            var parameters = new DynamicParameters();
        //            parameters.Add("@Username", user.Username);
        //            parameters.Add("@Password", user.Password);
        //            parameters.Add("@Email", user.Email);
        //            parameters.Add("@UserType", user.UserType);

        //            int affectedRows = await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

        //            bool isSigned = (affectedRows > 0); 

        //            return isSigned;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return false; 
        //    }
        //}


    }
}
