using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FastBuffer;

public ref struct FastBufferWriter
{
    private Span<byte> _buffer;
    private int _position;
    public FastBufferWriter(Span<byte> buffer)
    {
        _buffer = buffer;
        _position = 0;
    }

    public int WrittenCount => _position;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteInt32(int value)
    {
        var slice = _buffer.Slice(_position);
        BinaryPrimitives.WriteInt32LittleEndian(slice, value);
        _position += sizeof(int);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRawString(ReadOnlySpan<char> value)
    {
        var slice = _buffer.Slice(_position);
        int bytesWritten = System.Text.Encoding.UTF8.GetBytes(value, slice);
         _position += bytesWritten;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> WrittenSpan()
    {
        return _buffer.Slice(0, _position);
    }
}

