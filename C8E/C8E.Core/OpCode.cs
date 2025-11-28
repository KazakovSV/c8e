namespace C8E.Core;

internal readonly struct OpCode(ushort raw)
{
    public byte Category => (byte)(raw >> 12);

    public ushort NNN => (ushort)(raw & 0x0FFF);

    public byte NN => (byte)(raw & 0x00FF);

    public byte N => (byte)(raw & 0x000F);

    public byte X => (byte)((raw & 0x0F00) >> 8);

    public byte Y => (byte)((raw & 0x00F0) >> 4);

    public override string ToString() => $"0x{raw:X4}";
}
