using Orleans;

namespace Ardalis.SmartEnum.Orleans;

[GenerateSerializer, Immutable]
public struct SmartEnumSurrogate<TEnum, TValue>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    public TValue Value;
}

[RegisterConverter]
public sealed class SmartEnumSurrogateConverter<TEnum, TValue> : IConverter<TEnum, SmartEnumSurrogate<TEnum, TValue>>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    public TEnum ConvertFromSurrogate(in SmartEnumSurrogate<TEnum, TValue> surrogate)
    {
        return SmartEnum<TEnum, TValue>.FromValue(surrogate.Value);
    }

    public SmartEnumSurrogate<TEnum, TValue> ConvertToSurrogate(in TEnum smartEnum)
    {
        return new SmartEnumSurrogate<TEnum, TValue> { Value = smartEnum.Value };
    }
}