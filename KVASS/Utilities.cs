﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KVASSNS
{
    public static class Utils
    {
        public static object GetMemberInfoValue(System.Reflection.MemberInfo member, object sourceObject)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            object newVal;
            if (member is System.Reflection.FieldInfo)
                newVal = ((System.Reflection.FieldInfo)member).GetValue(sourceObject);
            else
                newVal = ((System.Reflection.PropertyInfo)member).GetValue(sourceObject, null);
            return newVal;
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }
    }

    public static class TypeExtensions
    {

        public static T GetPublicValue<T>(this Type type, string name, object instance) where T : class
        {
            if (type == null) return null;
            return (T)Utils.GetMemberInfoValue(type.GetMember(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).FirstOrDefault(), instance);
        }

        public static object GetPrivateMemberValue(this Type type, string name, object instance, int index = -1)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            object value = Utils.GetMemberInfoValue(type.GetMember(name, flags).FirstOrDefault(), instance);
            if (value != null)
            {
                return value;
            }

            Logging.Log($"Could not get value by name '{name}', getting by index '{index}'");
            if (index >= 0)
            {
                List<MemberInfo> members = type.GetMembers(flags).ToList();
                if (members.Count > index)
                {
                    return Utils.GetMemberInfoValue(members[index], instance);
                }
            }
            throw new Exception($"No members found for name '{name}' at index '{index}' for type '{type}'");
        }
    }
}
