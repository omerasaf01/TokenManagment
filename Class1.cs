using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;

namespace NiafixAuthentication
{
    public class ExampleModel
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public long Permission { get; set; }
    }
    public class TokenManagment
    {
        public static List<Claim> TokenDecoder(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            Token = Token.ToString();
            var token = Token.Split(" ");

            var EncodedToken = handler.ReadJwtToken(token[1]);

            return EncodedToken.Claims.ToList();
        }
        
        public static string CreateToken(ExampleModel _model)
        {
            var bytes = Encoding.UTF8.GetBytes("7A24432646294A404E635266546A576E5A7234753778214125442A472D4B6150");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials cred  = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var Claims = new Claim[] 
            {
                new Claim("ID", _model.ID.ToString()),
                new Claim("Username", _model.Username),
                new Claim("Name", _model.Name),
                new Claim("Permission", _model.Permission.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: cred,
                claims: Claims
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}