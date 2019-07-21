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
            var userPasswordString = userInfo.User + "-" + userInfo.Password + "-" + DateTime.Now.ToUniversalTime().ToString();
            var authBytes = AuthenticationHelper.GetBytes(userPasswordString);
            var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(authBytes);
            var token = AuthenticationHelper.GetString(hashBytes);
            return Guid.NewGuid().ToString();
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
