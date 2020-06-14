using System;

namespace Application.Interfaces.Common
{
    public interface ITypeService
    {
        Type GetClassTypeByName(string className);
        Type GetInterfaceTypeFromClassByName(string interfaceName, Type classType);
    }
}
