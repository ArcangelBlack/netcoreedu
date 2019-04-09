using System;
using System.Collections.Generic;

namespace UsersAPIService.Models
{
    public partial class UsersModel
    {
        public UsersModel()
        {
            UsersRoles = new HashSet<UsersRolesModel>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }

        public ICollection<UsersRolesModel> UsersRoles { get; set; }
    }
}
