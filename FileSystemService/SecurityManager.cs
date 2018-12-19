using System;
using System.Linq;
using Contracts;

namespace SecurityService
{
    public class SecurityManager : ISecurityManager
    {
        private const string PasswordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public SecurityManager()
        {
        }

        public string GenerateRandomPassword(int length)
        {
            return new string(
                Enumerable.Range(1, length)
                    .Select(_ => PasswordChars[new Random(Guid.NewGuid().GetHashCode()).Next(PasswordChars.Length)])
                    .ToArray()
            );
        }
    }
}