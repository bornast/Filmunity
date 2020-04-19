using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Application.Helpers
{
    public static class Seed
    {
        private static IUnitOfWork _uow;
        public static void SeedCoreData(IUnitOfWork uow, IHashService hashService)
        {
            _uow = uow;
            
            if (_uow.Repository<Status>().Count(x => x.Id > 0).Result > 0)
                return;

            SeedStatus();
            SeedUserWithRoles(hashService);
            SeedGender();            
            SeedFilmType();
            SeedGenre();
            SeedCountry();
            SeedFilmRole();
            SeedLanguage();
            uow.Save();
        }

        private static void SeedStatus()
        {
            var statuses = new List<Status>();

            foreach (var statusName in Enum.GetNames(typeof(Common.Enums.Status)))
            {
                statuses.Add(new Status { Name = statusName });
            }

            _uow.Repository<Status>().AddRange(statuses);
        }

        private static void SeedUserWithRoles(IHashService hashService)
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

            _uow.Repository<Role>().AddRange(roles);
            _uow.Repository<User>().AddRange(users);
        }

        private static void SeedGender()
        {
            var genders = new List<Gender>();

            foreach (var genderName in Enum.GetNames(typeof(Common.Enums.Genders)))
            {
                genders.Add(new Gender { Name = genderName });
            }

            _uow.Repository<Gender>().AddRange(genders);
        }

        private static void SeedFilmType()
        {
            var filmTypes = new List<FilmType>();

            foreach (var filmTypeName in Enum.GetNames(typeof(Common.Enums.FilmTypes)))
            {
                filmTypes.Add(new FilmType { Name = filmTypeName.Replace("_", " ") });
            }

            _uow.Repository<FilmType>().AddRange(filmTypes);
        }

        private static void SeedGenre()
        {
            var genreData = File.ReadAllText(GetFilePath("Data\\GenreData.json"));

            var genres = JsonConvert.DeserializeObject<List<Genre>>(genreData);

            _uow.Repository<Genre>().AddRange(genres);
        }

        private static void SeedCountry()
        {
            var countryData = File.ReadAllText(GetFilePath("Data\\CountryData.json"));

            var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);

            _uow.Repository<Country>().AddRange(countries);
        }

        private static void SeedFilmRole()
        {
            var fileRoleData = File.ReadAllText(GetFilePath("Data\\FilmRoleData.json"));

            var filmRoles = JsonConvert.DeserializeObject<List<FilmRole>>(fileRoleData);

            _uow.Repository<FilmRole>().AddRange(filmRoles);
        }

        private static void SeedLanguage()
        {
            var languageData = File.ReadAllText(GetFilePath("Data\\LanguageData.json"));

            var languages = JsonConvert.DeserializeObject<List<Language>>(languageData);

            _uow.Repository<Language>().AddRange(languages);            
        }

        private static string GetFilePath(string relativeFilePath)
        {
            var solutionPath = Directory.GetCurrentDirectory()
                .Substring(0, Directory.GetCurrentDirectory().LastIndexOf("\\"));

            var projectPath = $"{solutionPath}\\{nameof(Application)}";

            return $"{projectPath}\\{relativeFilePath}";
        }
    }
}
