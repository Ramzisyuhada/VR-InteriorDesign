using System;
using System.Reflection;

public static class ObjectExtensions
{
    public static T GetNestedFieldOrProperty<T>(this object target, string targetPath)
    {
        foreach (string part in targetPath.Split('.'))
        {
            target = target.GetFieldOrProperty<T>(part);
        }
        return (T)target;
    }

    public static T GetFieldOrProperty<T>(this object target, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
    {
        try
        {
            return GetField<T>(target, name, bindingFlags);
        }
        catch (FieldNotFoundException)
        {
            try
            {
                return GetProperty<T>(target, name, bindingFlags);
            }
            catch (PropertyNotFoundException)
            {
                throw new FieldNorPropertyNotFoundException("Can not find the field nor property named '" + name + "' inside the object '" + target.GetType().Name + "'");
            }
        }
    }

    public static T GetField<T>(this object target, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = target.GetType().GetField(name, bindingFlags);

        if (field != null)
            return (T)field.GetValue(target);
        else
            throw new FieldNotFoundException("Can not find the field named '" + name + "' inside the object '" + target.GetType().Name + "'");
    }

    public static T GetProperty<T>(this object target, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
    {
        PropertyInfo property = target.GetType().GetProperty(name, bindingFlags);
        if (property != null)
            return (T)property.GetValue(target);
        else
            throw new PropertyNotFoundException("Can not find the property named '" + name + "' inside the object '" + target.GetType().Name + "'");
    }

    public static bool IsNumbericType(this object obj)
    {
        switch (Type.GetTypeCode(obj.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }
}