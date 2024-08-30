using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        public long UserId { get; private set; }

        [Range(1,50)] 
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public byte[] HashSalt { get; private set; }
        public string HashPassword { get; private set; }

        //"toorist or custom"
        public IdentityRole Role { get;private set; } = new IdentityRole("tourist");
        public List<PhyHost> LinksHosts { get; set; } = new List<PhyHost>();

        

        public User() { }

        public User(string username, string email, string password)
        {
            UserName = username;
            Email = email;
            HashSalt = RandomNumberGenerator.GetBytes(16);
            var rfc = new Rfc2898DeriveBytes(password, HashSalt, 100000, HashAlgorithmName.SHA256);
            Role.Name = "custom";
            var hash = rfc.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(hash, 0, hashBytes, 0, 20);
            Array.Copy(HashSalt, 0, hashBytes, 20, 16);
            HashPassword = Convert.ToBase64String(hashBytes);
        }
        public void ResetPassword(string password)
        {
            if (password is null or "")
            {
                throw new ArgumentNullException("Password can't be null");
            }
            HashSalt = RandomNumberGenerator.GetBytes(16);
            var rfc = new Rfc2898DeriveBytes(password, HashSalt, 100000, HashAlgorithmName.SHA256);
            
            var hash = rfc.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(hash, 0, hashBytes, 0, 20);
            Array.Copy(HashSalt, 0, hashBytes, 20, 16);
            HashPassword = Convert.ToBase64String(hashBytes);
            
        }

        public bool ValidatePassword(string password, byte[] salt)
        {
            if (password is null or "")
            {
                throw new ArgumentNullException("Password can't be null");
            }
            
            var rfc = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            
            var hash = rfc.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(hash, 0, hashBytes, 0, 20);
            Array.Copy(salt, 0, hashBytes, 20, 16);
            var thisHashPassword = Convert.ToBase64String(hashBytes);

            return thisHashPassword.Equals(HashPassword);
        }
    }
}
