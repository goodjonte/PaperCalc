using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using PaperCalc.Data;
using PaperCalc.Models;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PaperCalc.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        public readonly IConfiguration _configuration;
        public IndexModel(IConfiguration config, PaperCalcContext context)
        {
            _context = context;
            _configuration = config;
        }
        [BindProperty]
        public PaperCalc.DTOs.NewUserDTO User { get; set; } = default!;
        public void OnGet()
        {
        }
        public ActionResult OnPost(PaperCalc.DTOs.NewUserDTO user)
        {
            bool UserExists = _context.User.Any(x => x.UserName == user.UserName);
            if (!UserExists)//Checks if username is valid
            {
                return BadRequest("User not found!");
            }

            var userLoggingIn = _context.User.FirstOrDefault(x => x.UserName == user.UserName);//Gets the user from the db
            if (userLoggingIn != null)
            {
                if (userLoggingIn.PasswordHash != null && userLoggingIn.PasswordSalt != null)
                {
                    if (!VerifyPasswordHash(user.Password, userLoggingIn.PasswordHash, userLoggingIn.PasswordSalt))//checks if password is valid(hash and salt would come from db in real project(the users row))
                    {
                        return BadRequest("Wrong Password");
                    }
                }
            }
            
            string token = CreateToken(user);
            Response.Cookies.Append("Parrot", token);

            return RedirectToPage("../Index");
        }

        private string CreateToken(PaperCalc.DTOs.NewUserDTO userTokenRequest)
        {
            User? user = _context.User.FirstOrDefault(x => x.UserName == userTokenRequest.UserName);
            if (user == null)
            {
                return "User not found";
            }

            string userRoleString = user.Admin ? "0" : "1";
            
            List<Claim> claims = new()
            {
                new Claim("user", user.Id.ToString()),
                new Claim("Role", userRoleString)
            };

            var keyToken = _configuration.GetSection("AppSettings:Token").Value;
            if (keyToken != null)
            {
                //Creates the key from the token created in appsettings (also turns it into a byte array first)
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyToken));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creats the creds (pretty much just signature to the key)

                var token = new JwtSecurityToken( //Initialisez the JWT token (putting everything together)
                    issuer: "Parrot",
                    audience: "CreateDesignStudio",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),//Here is the token expiry (Currently just adding 24hours from creation)
                    signingCredentials: cred
                    );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); //Actualty writes the token now it can be passed to the client

                return jwt;
            }
            return "No KeyToken in Appsettings";
        }

        //Creates a Hash from the entered password then compares it to the existing hash (Would be from db in real example)
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }



    }
}
