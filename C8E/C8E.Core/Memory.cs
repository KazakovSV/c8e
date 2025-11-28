namespace C8E.Core;

internal sealed class Memory
{
    public const int DefaultSize = 4096;

    private readonly byte[] _data;

    public Memory(int size = DefaultSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(size);

        _data = new byte[size];
    }

    public byte this[int address]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(address, _data.Length);

            return _data[address];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(address, _data.Length);

            _data[address] = value;
        }
    }

    public ushort ReadOpCode(int address)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(address);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(address, _data.Length - 1);

        return (ushort)((_data[address] << 8) | _data[address + 1]);
    }

    public void Load(byte[] rom, int address)
    {
        ArgumentNullException.ThrowIfNull(rom);

        if (rom.Length == 0)
            throw new ArgumentException("ROM is empty.", nameof(rom));

        ArgumentOutOfRangeException.ThrowIfNegative(address);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(address, _data.Length - rom.Length);

        Array.Copy(rom, 0, _data, address, rom.Length);
    }

    public void Reset() => Array.Clear(_data);
}
