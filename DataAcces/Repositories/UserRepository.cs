
namespace DataAccesLayer.Repositories
{
    using DataAccesLayer.Helpers;
    using DataAccesLayer.Models;
    using DataAccesLayer.Repository.Intefaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly UsersDBContext _context;

        public UserRepository()
        {
            _context = new UsersDBContext();
        }

        public UserRepository(UsersDBContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> GetByIdAsync(int userId)
        {
            try
            {
                return await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(Users user)
        {
            try
            {
                _context.AddAsync(user);
                Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Users user)
        {
            try
            {
                _context.Update(user);
                Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(Users user)
        {
            try
            {
                _context.Remove(user);
                Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Save()
        {
            _context.SaveChangesAsync();
        }

        #region Test Mx

        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        #endregion
    }
}
