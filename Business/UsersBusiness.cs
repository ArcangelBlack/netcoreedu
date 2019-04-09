

namespace Business
{
    using BusinessLayer.Interfaces;
    using DataAccesLayer.Interfaces;
    using DataAccesLayer.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UsersBusiness : IUsersBusiness
    {
        private readonly IUsersData _usersDataService;
        public UsersBusiness(IUsersData usersDataService)
        {
            this._usersDataService = usersDataService;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _usersDataService.GetAllUsersAsync();
        }

        public async Task<Users> GetByIdAsync(int userId)
        {
            return await _usersDataService.GetById(userId);
        }

        public void InsertUser(Users user)
        {
            _usersDataService.Insert(user);
        }

        public void UpdateUser(Users user)
        {
            _usersDataService.Update(user);
        }
    }
}
