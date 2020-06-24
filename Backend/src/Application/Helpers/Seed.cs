using Application.Interfaces;
using Common.Libs;
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
            SeedEntityTypes();
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

            foreach (var status in EnumLibrary.GetIdAndNameDictionaryOfEnumType(typeof(Common.Enums.Status)))
            {
                statuses.Add(new Status { Id = status.Key, Name = status.Value });
            }

            _uow.Repository<Status>().AddRange(statuses);
        }

        private static void SeedEntityTypes()
        {
            var entityTypes = new List<EntityType>();

            foreach (var entityType in EnumLibrary.GetIdAndNameDictionaryOfEnumType(typeof(Common.Enums.EntityTypes)))
            {
                entityTypes.Add(new EntityType { Id = entityType.Key, Name = entityType.Value });
            }

            _uow.Repository<EntityType>().AddRange(entityTypes);
        }

        private static void SeedUserWithRoles(IHashService hashService)
        {
            var roles = new List<Role>();

            var users = new List<User>();

            foreach (var role in EnumLibrary.GetIdAndNameDictionaryOfEnumType(typeof(Common.Enums.Roles)))
            {
                var roleToAdd = new Role { Id = role.Key, Name = role.Value };

                roles.Add(roleToAdd);

                // TODO: set this password in config or something
                var password = hashService.CreatePasswordHash("password");
                var user = new User
                {
                    Username = role.Value,
                    PasswordHash = password.PasswordHash,
                    PasswordSalt = password.PasswordSalt,
                    Email = $"{role.Value}@testmail.com",
                    StatusId = (int)Common.Enums.Status.Activated,
                    Roles = new List<UserRole>
                    {
                        new UserRole { Role = roleToAdd }
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

            foreach (var gender in EnumLibrary.GetIdAndNameDictionaryOfEnumType(typeof(Common.Enums.Genders)))
            {
                genders.Add(new Gender { Id = gender.Key, Name = gender.Value });
            }

            _uow.Repository<Gender>().AddRange(genders);
        }

        private static void SeedFilmType()
        {
            var filmTypes = new List<FilmType>();

            foreach (var filmType in EnumLibrary.GetIdAndNameDictionaryOfEnumType(typeof(Common.Enums.FilmTypes)))
            {
                filmTypes.Add(new FilmType { Id = filmType.Key, Name = filmType.Value.Replace("_", " ") });
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
            // TODO: this souldn't be hardcoded, somehow get the solution name programmatically
            var solutionName = "Filmunity";

            var solutionPath = Directory.GetCurrentDirectory().Substring(0, 
                Directory.GetCurrentDirectory().LastIndexOf("Filmunity") + solutionName.Length);

            var projectPath = $"{solutionPath}\\Backend\\src\\{nameof(Application)}";

            return $"{projectPath}\\{relativeFilePath}";
        }
    }
}
