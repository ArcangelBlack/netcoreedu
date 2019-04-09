using System;
using System.Collections.Generic;

namespace DataAccesLayer.Models
{
    public partial class UsersRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}
