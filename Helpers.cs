using System;

namespace CryptoHashCalc
{
    public static class Helpers
    {
        /// <summary>
        /// Converts a byte array into a lowercase hexadecimal string with no separators.
        /// Primarily used to format hash digests into a human-readable form.
        /// </summary>
        /// <param name="bytes">The raw hash bytes.</param>
        /// <returns>Lowercase hexadecimal string representation of the input.</returns>
        public static string ByteArray2Hex(byte[] bytes)

        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Encodes a 32-bit unsigned integer into a 4-byte big-endian array.
        /// Used by CRC-32 finalization to match standard network-byte-order conventions.
        /// </summary>
        /// <param name="x">The 32-bit value to convert.</param>
        /// <returns>A 4-byte array in big-endian order.</returns>
        public static byte[] ToBigEndianBytes(UInt32 x)

        {
            return new byte[]
            {
                (byte)((x >> 24) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >> 8) & 0xff),
                (byte)(x & 0xff)
            };
        }

    }
}
