namespace Potion.Extensions;

public static class ByteExtensions
{
    public static string ToHexString(this byte b)
    {
        return $"0x{BitConverter.ToString(new[] { b })}";
    }
}