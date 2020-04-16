using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helpers
{
    public static class Seed
    {

        public static void SeedCoreData(IUnitOfWork uow, IHashService hashService)
        {
            if (uow.Repository<Status>().Count(x => x.Id > 0).Result > 0)
                return;

            SeedStates(uow);
            SeedUserWithRoles(uow, hashService);
            uow.Save();
        }

        private static void SeedStates(IUnitOfWork uow)
        {
            var statuses = new List<Status>();

            foreach (var statusName in Enum.GetNames(typeof(Common.Enums.Status)))
            {
                statuses.Add(new Status { Name = statusName });
            }

            uow.Repository<Status>().AddRange(statuses);
        }

        private static void SeedUserWithRoles(IUnitOfWork uow, IHashService hashService)
        {
            var roles = new List<Role>();

            var users = new List<User>();

            foreach (var roleName in Enum.GetNames(typeof(Common.Enums.Roles)))
            {
                var role = new Role { Name = roleName };

                roles.Add(role);

                // TODO: set this password in config or something
                var password = hashService.CreatePasswordHash("password");
                var user = new User
                {
                    Username = roleName,
                    PasswordHash = password.PasswordHash,
                    PasswordSalt = password.PasswordSalt,
                    Email = $"{roleName}@testmail.com",
                    StatusId = (int)Common.Enums.Status.Activated,
                    Roles = new List<UserRole>
                    {
                        new UserRole { Role = role }
                    }
                };

                users.Add(user);
            }

            uow.Repository<Role>().AddRange(roles);
            uow.Repository<User>().AddRange(users);
        }

    }
}
