namespace Ardalis.SmartEnum
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class SmartEnumExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSmartEnum(this Type type) =>
            IsSmartEnum(type, out var _);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericArguments"></param>
        /// <returns></returns>
        public static bool IsSmartEnum(this Type type, out Type[] genericArguments)
        {
            if (type is null || type.IsAbstract || type.IsGenericTypeDefinition)
            {
                genericArguments = null;
                return false;
            }

            do
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(SmartEnum<,>))
                {
                    genericArguments = type.GetGenericArguments();
                    return true;
                }

                type = type.BaseType;
            }
            while (type is not null);

            genericArguments = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="enums"></param>
        /// <returns></returns>
        public static bool TryGetValues(this Type type, out IEnumerable<object> enums)
        {
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SmartEnum<,>) || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SmartFlagEnum<,>))
                {
                    var listPropertyInfo = type.GetProperty("List", BindingFlags.Public | BindingFlags.Static);
                    enums = (IEnumerable<object>)listPropertyInfo.GetValue(type, null);
                    return true;
                }
                type = type.BaseType;
            }

            enums = null;
            return false;
        }
    }
}
