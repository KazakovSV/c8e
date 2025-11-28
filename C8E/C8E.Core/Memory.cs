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
            EnsureValidAddress(address);
            return _data[address];
        }
        set
        {
            EnsureValidAddress(address);
            _data[address] = value;
        }
    }

    public ushort ReadOpCode(int address)
    {
        EnsureValidAddress(address);
        EnsureValidAddress(address + 1);

        return (ushort)((_data[address] << 8) | _data[address + 1]);
    }

    public void Load(byte[] rom, int address)
    {
        ArgumentNullException.ThrowIfNull(rom);

        if (rom.Length == 0)
            throw new ArgumentException("ROM is empty.", nameof(rom));

        EnsureValidAddress(address);

        if (address < 0 || address + rom.Length > _data.Length)
            throw new ArgumentOutOfRangeException(nameof(address), "ROM does not fit into memory.");

        Array.Copy(rom, 0, _data, address, rom.Length);
    }

    public void Reset() => Array.Clear(_data);

    private void EnsureValidAddress(int address)
    {
        if (address < 0 || address >= _data.Length)
            throw new ArgumentOutOfRangeException(nameof(address), $"Address {address} is out of range.");
    }
}
