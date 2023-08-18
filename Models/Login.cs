using PaperCalc.Data;
using System.Security.Cryptography;

namespace PaperCalc.Models
{
    public class Login
    {
        public Guid Id { get; set; }
        public string Hash { get; set; } = default!;
        public string Salt { get; set; } = default!;

        public static bool ValidatePassword(PaperCalcContext _context,string passwordEntered, bool isBase64 = true)
        {
            if(isBase64)
            {
                passwordEntered = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(passwordEntered));
            }
            PaperCalc.Models.Login? log = _context.Login.FirstOrDefault();
            if (log == null)
            {
                return false;
            }
            byte[] salt = Convert.FromBase64String(log.Salt);
            byte[] hash = Convert.FromBase64String(log.Hash);

            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordEntered));

            return computedHash.SequenceEqual(hash);
        }
    }
}
