using Dapper;
using InventoryManagementSystem.Models;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagementSystem.Repository
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly string _connectionString;

        public SignUpRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conn");
        }

        public void AddUser(Users user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();


                    string storedProcedureName = "SignupUser";

                    var parameters = new DynamicParameters();
                    parameters.Add("@Username", user.Username);
                    parameters.Add("@Password", user.Password);
                    parameters.Add("@Email", user.Email);
                    parameters.Add("@UserType", user.UserType);
                    connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred while adding a user: " + ex.Message);
            }
        }
    }
}
