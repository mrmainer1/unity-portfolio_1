using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Project.EntenEller.Base.Scripts.Advanced.Serializations
{
    public static class EEByteSerialization
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SerializeUnsafe<T>(ref byte[] buffer, ref int index, T value) where T : unmanaged
        {
            fixed (byte* ptr = &buffer[index])
            {
                *(T*)ptr = value;
            }
            index += sizeof(T);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T DeserializeUnsafe<T>(ref byte[] buffer, ref int index) where T : unmanaged
        {
            fixed (byte* ptr = &buffer[index])
            {
                index += sizeof(T);
                return *(T*)ptr;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SerializeString(ref byte[] buffer, ref int index, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            var stringBytes = Encoding.UTF8.GetBytes(value);
            var length = stringBytes.Length;
            
            if (buffer.Length < index + sizeof(int) + length) Array.Resize(ref buffer, index + sizeof(int) + length);
            
            fixed (byte* ptr = &buffer[index])
            {
                *(int*)ptr = length;
            }
            index += sizeof(int);
            
            Buffer.BlockCopy(stringBytes, 0, buffer, index, length);
            index += length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe string DeserializeString(ref byte[] buffer, ref int index)
        {
            if (buffer.Length < index + sizeof(int)) throw new InvalidOperationException("Buffer too small to read string length.");

            int length;
            fixed (byte* ptr = &buffer[index])
            {
                length = *(int*)ptr;
            }
            index += sizeof(int);

            if (buffer.Length < index + length) throw new InvalidOperationException("Buffer too small to read string data.");
            
            var result = Encoding.UTF8.GetString(buffer, index, length);
            index += length;

            return result;
        }

    }
}
