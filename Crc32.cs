using System;
using System.Security.Cryptography;

namespace CryptoHashCalc
{
    /// <summary>
    /// CRC-32 hash algorithm implemented as a <see cref="HashAlgorithm"/> subclass.  
    /// Although not cryptographically secure, CRC-32 is included for fast
    /// integrity checking and compatibility with common tooling (SFV, archives, etc.).
    /// </summary>
    public class Crc32 : HashAlgorithm
    {
        public const UInt32 DefaultPolynomial = 0xedb88320;
        public const UInt32 DefaultSeed = 0xffffffff;

        private UInt32 _hash;
        private readonly UInt32 _seed;
        private readonly UInt32[] _table;
        private static UInt32[] _defaultTable;

        public Crc32()
        {
            _table = InitTable(DefaultPolynomial);
            _seed = DefaultSeed;
            Initialize();
        }

        /// <summary>
        /// Initializes the CRC-32 accumulator to the default seed.
        /// </summary>
        public override void Initialize()
        {
            _hash = _seed;
        }

        /// <summary>
        /// Processes a block of input data and updates the ongoing CRC value.
        /// </summary>
        /// <param name="array">Input buffer.</param>
        /// <param name="ibStart">Offset where processing begins.</param>
        /// <param name="cbSize">Number of bytes to process.</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)

        {
            _hash = CalcHash(_table, _hash, array, ibStart, cbSize);
        }

        /// <summary>
        /// Finalizes the CRC-32 computation by producing the big-endian 32-bit checksum.
        /// </summary>
        /// <returns>Byte array containing the 4-byte CRC value.</returns>
        protected override byte[] HashFinal()

        {
            byte[] hashBuffer = Helpers.ToBigEndianBytes(~_hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize => 32;

        /// <summary>
        /// Computes a full CRC-32 checksum of the provided buffer in one step.
        /// </summary>
        /// <param name="buffer">Data to hash.</param>
        /// <returns>32-bit CRC result.</returns>
        public static UInt32 Compute(byte[] buffer)

        {
            return ~CalcHash(InitTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Generates the CRC lookup table for the specified polynomial.  
        /// The table is cached for the default polynomial.
        /// </summary>
        private static UInt32[] InitTable(UInt32 polynomial)

        {
            if (polynomial == DefaultPolynomial && _defaultTable != null)
                return _defaultTable;

            UInt32[] createTable = new UInt32[256];
            for (int i = 0; i < 256; i++)
            {
                UInt32 entry = (UInt32)i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                _defaultTable = createTable;

            return createTable;
        }

        /// <summary>
        /// Internal CRC-32 accumulator. Processes a buffer range and returns
        /// the new CRC value.
        /// </summary>
        private static UInt32 CalcHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)

        {
            UInt32 crc = seed;
            for (int i = start; i < size; i++)
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            return crc;
        }
    }
}
