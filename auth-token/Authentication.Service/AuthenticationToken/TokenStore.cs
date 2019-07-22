using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    internal class TokenStore
    {
        private static TokenStore _instance = new TokenStore();
        internal static TokenStore Instance
        {
            get
            {
                return _instance;
            }
        }

        private int TOKEN_EXPIRY_PERIOD = 60; // in minutes
        private ConcurrentDictionary<string, UserSession> UserTokens { get; set; }

        private TokenStore()
        {
            this.UserTokens = new ConcurrentDictionary<string, UserSession>();
        }

        /// <summary>
        /// Removes the token & associated session
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal UserInfo RemoveToken(string token)
        {
            UserSession userSession = null;
            var removed = this.UserTokens.TryRemove(token, out userSession);
            return userSession.UserInfo;
        }

        /// <summary>
        /// Get new token for the user
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        internal string GetNewToken(UserInfo userInfo)
        {
            var token = this.ComputeToken(userInfo);
            this.UserTokens.TryAdd(token, new UserSession()
            {
                StartTime = DateTime.Now,
                UserInfo = userInfo,
                Token = token
            });
            return token;
        }

        /// <summary>
        /// Gets the user for this token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal UserSession GetSession(string token)
        {
            var newToken = this.GetNewToken(token);
            return this.UserTokens.ContainsKey(token) ? this.UserTokens[token] : null;
        }

        /// <summary>
        /// Get new token based on old token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        internal string GetNewToken(string oldToken)
        {
            UserSession userSession = null;
            var newToken = this.IsTokenExpired(oldToken) ? string.Empty : oldToken;
            if(oldToken.Equals(newToken))
            {
                return oldToken;
            }
            else if(string.IsNullOrEmpty(newToken) && this.UserTokens.TryRemove(oldToken, out userSession) && (userSession != null))
            {
                newToken = this.ComputeToken(userSession.UserInfo);
                userSession.Token = newToken;
                this.UserTokens.TryAdd(newToken, userSession);
            }
            else
            {
                throw new InvalidTokenException(oldToken);
            }
            return newToken;
        }

        /// <summary>
        /// Computes the token based on user information
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string ComputeToken(UserInfo userInfo)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.GenerateKey();
            var tokenString = userInfo.User + "-" + DateTime.Now.Ticks + "-" + AuthenticationHelper.GetString(tdes.Key);
            var authBytes = AuthenticationHelper.GetBytes(tokenString);
            var token = new StringBuilder();
            foreach(var authByte in authBytes)
            {
                // Pick only alphanumeric characters: 65 - 90 && 97 - 122 && 48 - 57
                if((authByte >= 65 && authByte <= 90) || (authByte >= 97 && authByte <= 122) || (authByte >= 48 && authByte <= 57))
                {
                    token.Append((char)authByte);
                }
            }
            return token.ToString();
        }

        /// <summary>
        /// Check if token expired
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool IsTokenExpired(string token)
        {
            var isTokenExpired = false;
            if(this.UserTokens.ContainsKey(token))
            {
                var userSession = this.UserTokens[token];
                var currentTime = DateTime.Now;
                isTokenExpired = (currentTime - userSession.StartTime).Minutes >= TOKEN_EXPIRY_PERIOD;
            }
            return isTokenExpired;
        }
    }
}
