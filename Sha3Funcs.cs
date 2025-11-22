using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;

namespace CryptoHashCalc
{
    /// <summary>
    /// SHA-3 (Keccak) implementation built on top of BouncyCastle's <see cref="Sha3Digest"/>.  
    /// Acts as a drop-in <see cref="HashAlgorithm"/> compatible wrapper,
    /// supporting bit lengths 256/384/512.
    /// </summary>
    public class SHA3Managed : HashAlgorithm

    {
        private readonly Sha3Digest _sha3Digest;

        /// <summary>
        /// Creates a new SHA-3 instance with the specified output size.
        /// </summary>
        /// <param name="bitLength">Hash length in bits (256/384/512).</param>
        public SHA3Managed(int bitLength)

        {
            _sha3Digest = new Sha3Digest(bitLength);
            HashSizeValue = bitLength;
        }

        /// <summary>
        /// Resets the internal Keccak state.
        /// </summary>
        public override void Initialize()

        {
            _sha3Digest.Reset();
        }

        /// <summary>
        /// Feeds a block of data into the SHA-3 sponge function.
        /// </summary>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)

        {
            _sha3Digest.BlockUpdate(array, ibStart, cbSize);
        }

        /// <summary>
        /// Finalizes the SHA-3 computation and returns the resulting digest.
        /// </summary>
        /// <returns>Hash byte array.</returns>
        protected override byte[] HashFinal()

        {
            var result = new byte[_sha3Digest.GetDigestSize()];
            _sha3Digest.DoFinal(result, 0);
            return result;
        }
    }
}
