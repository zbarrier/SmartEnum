using Orleans;

namespace Ardalis.SmartEnum.Orleans;

[GenerateSerializer, Immutable]
public struct SmartFlagEnumSurrogate<TEnum, TValue>
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    public TValue Value;
}

[RegisterConverter]
public sealed class SmartFlagEnumSurrogateConverter<TEnum, TValue> : IConverter<TEnum, SmartFlagEnumSurrogate<TEnum, TValue>>
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    public TEnum ConvertFromSurrogate(in SmartFlagEnumSurrogate<TEnum, TValue> surrogate)
    {
        return SmartFlagEnum<TEnum, TValue>.DeserializeValue(surrogate.Value);
    }

    public SmartFlagEnumSurrogate<TEnum, TValue> ConvertToSurrogate(in TEnum smartEnum)
    {
        return new SmartFlagEnumSurrogate<TEnum, TValue> { Value = smartEnum.Value };
    }
}