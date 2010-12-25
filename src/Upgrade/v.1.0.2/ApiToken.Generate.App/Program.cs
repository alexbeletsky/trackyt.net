using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Services;

namespace ApiToken.Generate.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trackyt.net - upgrade for v.1.0.2.\nGenerate API tokens for all existing users.\n");

            try
            {
                // repositories
                var usersRepository = new UsersRepository();

                // services
                var hash = new HashService();

                GenerateApiToken(usersRepository, hash);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

        }

        private static void GenerateApiToken(UsersRepository usersRepository, HashService hash)
        {
            foreach (var user in usersRepository.Users.Where(u => u.Temp == false))
            {
                if (string.IsNullOrEmpty(user.ApiToken))
                {
                    var apiToken = hash.CreateApiToken(user.Email, user.PasswordHash);
                    user.ApiToken = apiToken;

                    usersRepository.Save(user);

                    Console.WriteLine(string.Format("Generated API token for {0}", user.Email));
                }
            }
        }
    }
}
