using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repository
{
    public interface ISignUpRepository
    {

        public Task<bool> AddUser(Users user);
    }
}
