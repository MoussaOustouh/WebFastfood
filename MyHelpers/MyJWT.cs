using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;


namespace WebFastfood.MyHelpers
{
    public class MyJWT
    {
        private readonly static string key = "my secret key 12345";
        private readonly static string issuer = "Fast food Web App";
        private readonly static string audience = "Fast food Web App";
        private readonly static string securityAlgorithm = SecurityAlgorithms.HmacSha256;
        private readonly static IEnumerable<string> algos = new string[] { MyJWT.securityAlgorithm };


        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MyJWT.key));

            return symmetricSecurityKey;
        }

        public static SigningCredentials GetSigningCredentials()
        {
            SigningCredentials credentials = new SigningCredentials(MyJWT.GetSymmetricSecurityKey(), MyJWT.securityAlgorithm);

            return credentials;
        }

        public static string GenerateJWT(List<Claim> listClaims)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            listClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = null,
                Subject = new ClaimsIdentity(listClaims),
                Issuer = MyJWT.issuer,
                Audience = MyJWT.audience,
                SigningCredentials = MyJWT.GetSigningCredentials(),
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public static bool ValidJWT(string jwt)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(jwt))
            {
                TokenValidationParameters validationParams = new TokenValidationParameters
                {
                    RequireExpirationTime = false,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    ValidAlgorithms = MyJWT.algos,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = MyJWT.issuer,
                    ValidAudience = MyJWT.audience,
                    IssuerSigningKey = MyJWT.GetSymmetricSecurityKey()
                };

                try
                {
                    tokenHandler.ValidateToken(jwt, validationParams, out SecurityToken validatedToken);
                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public static List<Claim> GetClaims(string jwt)
        {
            List<Claim> list;

            try
            {
                JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: jwt);
                list = token.Claims.ToList();
            }
            catch (ArgumentException e)
            {
                list = new List<Claim>();
            }

            return list;
        }




    }
}