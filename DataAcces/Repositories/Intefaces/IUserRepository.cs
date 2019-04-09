
namespace DataAccesLayer.Repository.Intefaces
{
    using DataAccesLayer.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetByIdAsync(int userId);
        void Insert(Users user);
        void Update(Users user);
        void Delete(Users user);
        void Save();

        #region Test Mx

        Users GetById(int id);

        Users Create(Users user, string password);

        Users Authenticate(string username, string password);

        #endregion
    }
}
