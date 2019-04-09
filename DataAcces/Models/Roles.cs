using System;
using System.Collections.Generic;

namespace DataAccesLayer.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UsersRoles = new HashSet<UsersRoles>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
