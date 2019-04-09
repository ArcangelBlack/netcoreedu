using System;
using System.Collections.Generic;

namespace UsersAPIService.Models
{
    public partial class UsersRolesModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public RolesModel Role { get; set; }
        public UsersModel User { get; set; }
    }
}
