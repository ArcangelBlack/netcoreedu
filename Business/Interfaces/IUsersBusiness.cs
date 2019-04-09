using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUsersBusiness
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetByIdAsync(int userId);
        void InsertUser(Users user);
        void UpdateUser(Users user);
    }
}
