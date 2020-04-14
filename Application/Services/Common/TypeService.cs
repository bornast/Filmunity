using Application.Interfaces.Common;
using Ardalis.GuardClauses;
using System;
using System.Linq;

namespace Application.Services.Common
{
    public class TypeService : ITypeService
    {
        public Type GetInterfaceTypeFromClassByName(string interfaceName, Type classType)
        {
            var validatorInterfaceType = classType.GetInterfaces()
                .FirstOrDefault(x => x.FullName.Contains(interfaceName));

            Guard.Against.Null(validatorInterfaceType, nameof(validatorInterfaceType));

            return validatorInterfaceType;
        }

        public Type GetClassTypeByName(string className)
        {
            var type = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == className);

            Guard.Against.Null(type, nameof(type));

            return type;
        }


    }
}
