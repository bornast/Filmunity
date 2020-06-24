using System;
using System.Collections.Generic;

namespace Common.Libs
{
    public static class EnumLibrary
    {
        public static Dictionary<int, string> GetIdAndNameDictionaryOfEnumType(Type enumType)
        {
            var result = new Dictionary<int, string>();

            foreach (var entityTypeName in Enum.GetNames(enumType))
            {
                var enumId = (int)Enum.Parse(enumType, entityTypeName);

                result.Add(enumId, entityTypeName);
            }

            return result;
        }

    }
}
