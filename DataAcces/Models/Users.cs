using System;
using System.Collections.Generic;

namespace DataAccesLayer.Models
{
    public partial class Users
    {
        public Users()
        {
            UsersRoles = new HashSet<UsersRoles>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
