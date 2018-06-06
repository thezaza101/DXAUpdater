/* PLACEHOLDER
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DXAUpdater.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace DXAUpdater.Controllers
{

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UpdatedDataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UpdatedDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var userName = User.Identity.Name;

            return Ok("Oh Gawd one of them got through, ABORT MISSION, ABORT MISSION");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (request.Username == "1" && request.Password == "2")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim("CompletedBasicTraining", ""),
                    new Claim(ClaimTypes.System, new DateTime(2017,12,1).ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityTokenKey"]));
                var ec = new EncryptingCredentials(securityKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes192CbcHmacSha384);
                    

                var jwt = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    subject: new ClaimsIdentity(claims),
                    issuedAt: DateTime.Now,
                    notBefore:DateTime.Now,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds,
                    encryptingCredentials: ec
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new
                {
                    token = tokenString
                    
                    
                    // EncryptToken(tokenString.Split('.')[0],"F360415FAD41EC04448F0E8719330CC7")+"."+
                    // EncryptToken(tokenString.Split('.')[1],"F360415FAD41EC04448F0E8719330CC7")+"."+
                    // EncryptToken(tokenString.Split('.')[2],"F360415FAD41EC04448F0E8719330CC7")

                });
            }

            return BadRequest("Could not verify username and password");
        }

        public static string EncryptToken(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptToken(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
    public class CustomJwtSecurityTokenHandler : ISecurityTokenValidator
    {
        private int _maxTokenSizeInBytes = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
        private JwtSecurityTokenHandler _tokenHandler;

        public CustomJwtSecurityTokenHandler()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken
        {
            get
            {
                return true;
            }
        }

        public int MaximumTokenSizeInBytes
        {
            get
            {
                return _maxTokenSizeInBytes;
            }

            set
            {
                _maxTokenSizeInBytes = value;
            }
        }

        public bool CanReadToken(string securityToken)
        {
            string x = GetDecryptedToken(securityToken);
            var y =_tokenHandler.CanReadToken(x);
            return y;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var principal = _tokenHandler.ValidateToken(GetDecryptedToken(securityToken), validationParameters, out validatedToken);

            return principal;
        }

        public string GetDecryptedToken(string encryptToken)
        {
            string x1 = AuthController.DecryptToken(encryptToken.Split(".")[0],"F360415FAD41EC04448F0E8719330CC7");
            string x2 = AuthController.DecryptToken(encryptToken.Split(".")[1],"F360415FAD41EC04448F0E8719330CC7");
            string x3 = AuthController.DecryptToken(encryptToken.Split(".")[2],"F360415FAD41EC04448F0E8719330CC7");
            return x1+"."+x2+"."+x3;
        }
    }
}
*/