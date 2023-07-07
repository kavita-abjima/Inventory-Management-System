using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Infrastructure
{
    public interface ISignUpRepository
    {

        public Task<bool> AddUser(Users user);

        public Task<bool> LoginUser(Login login);

        public Task<bool> CheckUserExists(string email);
    }
}
