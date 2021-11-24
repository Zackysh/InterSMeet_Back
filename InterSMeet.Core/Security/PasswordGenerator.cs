using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace InterSMeet.Core.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public IConfiguration Configuration { get; set; }
        public PasswordGenerator(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string Hash(string password)
        {
            // generate a 128-bit salt
            byte[] salt = Encoding.UTF8.GetBytes(Configuration["Salt"]);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 98765,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public bool CompareHash(string attemptedPassword, string hash)
        {
            return hash == Hash(attemptedPassword);
        }
    }
}

