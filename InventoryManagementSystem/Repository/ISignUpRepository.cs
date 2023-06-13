using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repository
{
    public interface ISignUpRepository
    {

        public Task<bool> AddUser(Users user);

        public Task<bool> LoginUser(string username, string password, string userType);
    }
}
