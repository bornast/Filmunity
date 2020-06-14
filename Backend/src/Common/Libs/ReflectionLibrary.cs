using Mono.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Libs
{
    public class ReflectionLibrary
    {
        public static void SetValueToReadonlyProperty(object obj, Type objType, string objProperty, object objValue)
        {
            PropertyInfo nameProperty = objType.GetProperty(objProperty);
            FieldInfo nameField = nameProperty.GetBackingField();
            nameField.SetValue(obj, objValue);
        }
    }
}
