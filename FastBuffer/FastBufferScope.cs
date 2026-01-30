using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace FastBuffer;

public ref struct FastBufferScope
{
    private byte[] _rentedArray;

    private FastBufferWriter _writer;
    public ReadOnlySpan<byte> WrittenSpan => _writer.WrittenSpan();
    public FastBufferScope(int initialCapacity)
    {
        _rentedArray = ArrayPool<byte>.Shared.Rent(initialCapacity);
        _writer = new FastBufferWriter(_rentedArray);
    }
    public void WriteInt32(int value)
    {
        _writer.WriteInt32(value);
    }
    public void WriteRawString(ReadOnlySpan<char> value)
    {
        _writer.WriteRawString(value); 
    }
    public void Dispose()
    {
        if (_rentedArray != null)
        {
            ArrayPool<byte>.Shared.Return(_rentedArray);
            _rentedArray = null;
        }
    }
}

