using System;

public class NumericType
{
    public object value { get; private set; }
    public Type type { get; private set; }

    public NumericType(object obj)
    {
        if (!obj.IsNumbericType())
            throw new NumericTypeExpectedException("The type of object in the NumericType constructor must be numeric.");

        value = obj;
        type = obj.GetType();
    }

    public override bool Equals(object obj) { return obj == value; }
    public override int GetHashCode() { return value.GetHashCode(); }
    public override string ToString() { return value.ToString(); }

    public static bool operator <(NumericType left, NumericType right)
    {
        object leftValue = left.value;
        object rightValue = right.value;

        switch (Type.GetTypeCode(left.type))
        {
            case TypeCode.Byte: return (byte)leftValue < (byte)rightValue;
            case TypeCode.SByte: return (sbyte)leftValue < (sbyte)rightValue;
            case TypeCode.UInt16: return (ushort)leftValue < (ushort)rightValue;
            case TypeCode.UInt32: return (uint)leftValue < (uint)rightValue;
            case TypeCode.UInt64: return (ulong)leftValue < (ulong)rightValue;
            case TypeCode.Int16: return (short)leftValue < (short)rightValue;
            case TypeCode.Int32: return (int)leftValue < (int)rightValue;
            case TypeCode.Int64: return (long)leftValue < (long)rightValue;
            case TypeCode.Decimal: return (decimal)leftValue < (decimal)rightValue;
            case TypeCode.Double: return (double)leftValue < (double)rightValue;
            case TypeCode.Single: return (float)leftValue < (float)rightValue;
        }
        throw new NumericTypeExpectedException("The compared numeric types is invalid.");
    }

    public static bool operator >(NumericType left, NumericType right)
    {
        object leftValue = left.value;
        object rightValue = right.value;

        switch (Type.GetTypeCode(left.type))
        {
            case TypeCode.Byte: return (byte)leftValue > (byte)rightValue;
            case TypeCode.SByte: return (sbyte)leftValue > (sbyte)rightValue;
            case TypeCode.UInt16: return (ushort)leftValue > (ushort)rightValue;
            case TypeCode.UInt32: return (uint)leftValue > (uint)rightValue;
            case TypeCode.UInt64: return (ulong)leftValue > (ulong)rightValue;
            case TypeCode.Int16: return (short)leftValue > (short)rightValue;
            case TypeCode.Int32: return (int)leftValue > (int)rightValue;
            case TypeCode.Int64: return (long)leftValue > (long)rightValue;
            case TypeCode.Decimal: return (decimal)leftValue > (decimal)rightValue;
            case TypeCode.Double: return (double)leftValue > (double)rightValue;
            case TypeCode.Single: return (float)leftValue > (float)rightValue;
        }
        throw new NumericTypeExpectedException("Please compare valid numeric types.");
    }

    public static bool operator ==(NumericType left, NumericType right)
    {
        return !(left > right) && !(left < right);
    }

    public static bool operator !=(NumericType left, NumericType right)
    {
        return !(left > right) || !(left < right);
    }

    public static bool operator <=(NumericType left, NumericType right)
    {
        return left == right || left < right;
    }

    public static bool operator >=(NumericType left, NumericType right)
    {
        return left == right || left > right;
    }

}