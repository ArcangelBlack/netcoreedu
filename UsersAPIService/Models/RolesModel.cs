using System;
using System.Collections.Generic;

namespace UsersAPIService.Models
{
    public partial class RolesModel
    {
        public RolesModel()
        {
            UsersRoles = new HashSet<UsersRolesModel>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<UsersRolesModel> UsersRoles { get; set; }
    }
}
