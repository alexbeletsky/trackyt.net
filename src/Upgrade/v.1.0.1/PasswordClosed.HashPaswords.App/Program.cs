using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Services;

namespace PasswordClosed.HashPaswords.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trackyt.net - upgrade for v.1.0.1.\nHash all user passwords.\n");

            try
            {
                // repositories
                var usersRepository = new UsersRepository();

                // services
                var hash = new HashService();

                HashAllPasswords(usersRepository, hash);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static void HashAllPasswords(UsersRepository usersRepository, HashService hash)
        {
            foreach (var user in usersRepository.Users.Where(u => u.Temp == false))
            {
                if (string.IsNullOrEmpty(user.PasswordHash))
                {
                    var passwordHash = hash.CreateMD5Hash(user.Password);
                    user.PasswordHash = passwordHash;

                    usersRepository.SaveUser(user);

                    Console.WriteLine(string.Format("Hashed password for {0}", user.Email));
                }
            }
        }
    }
}
