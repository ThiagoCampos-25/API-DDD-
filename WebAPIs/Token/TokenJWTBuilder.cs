﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebAPIs.Token
{
    public class TokenJWTBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiyInMinutes = 5;


        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaims(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiyInMinutes),
                signingCredentials: new SigningCredentials(
                    this.securityKey,
                    SecurityAlgorithms.HmacSha256)
                ); 
            
            return new TokenJWT(token);
        }
    }
}
