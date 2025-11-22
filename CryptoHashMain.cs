using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace CryptoHashCalc
{
    public partial class CryptoHashMain : Form
    {
        public CryptoHashMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Environment.GetCommandLineArgs();
        }

        /// <summary>
        /// Main hashing entry point for CryptoHashCalc.  
        /// Provides a generic dispatcher that selects the requested hash algorithm
        /// (CRC32, MD5, SHA-2, SHA-3).  
        /// SHA-3 algorithms depend on BouncyCastle; if the DLL is missing, an
        /// error message is shown and null is returned.
        /// </summary>
        public static string CalcHash(string filePath, string hashAlgorithmName)
        {   
            
            if (hashAlgorithmName.StartsWith("SHA3-"))
            {
                string bouncyDLLFilename = "BouncyCastle.Crypto.dll";
                string exeDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string bouncyDLLPath = Path.Combine(exeDirectory, bouncyDLLFilename);

                if (!File.Exists(bouncyDLLPath))
                {
                    MessageBox.Show($"Please locate {bouncyDLLPath} in order to use SHA-3 algorithms.", "Error");
                    return null;
                }
            }

            using (var hashAlgorithm = CreateHashAlgorithm(hashAlgorithmName))
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Helpers.ByteArray2Hex(hashAlgorithm.ComputeHash(stream));
                }
            }
        }

        /// <summary>
        /// Factory method for constructing the appropriate <see cref="HashAlgorithm"/>
        /// implementation based on user-supplied algorithm name.
        /// </summary>
        /// <param name="name">Algorithm identifier (e.g. "SHA256", "CRC32").</param>
        /// <returns>
        /// A concrete <see cref="HashAlgorithm"/> instance, or null if the
        /// algorithm is unsupported.
        /// </returns>
        private static HashAlgorithm CreateHashAlgorithm(string name)
        {
            switch (name.ToUpperInvariant())
            {
                case "CRC32":
                    return new Crc32();
                case "MD5":
                    return MD5.Create();
                case "SHA256":
                    return SHA256.Create();
                case "SHA384":
                    return SHA384.Create();
                case "SHA512":
                    return SHA512.Create();
                case "SHA3-256":
                    return new SHA3Managed(256);
                case "SHA3-384":
                    return new SHA3Managed(384);
                case "SHA3-512":
                    return new SHA3Managed(512);
                default:
                    MessageBox.Show($"The hash algorithm '{name}' is not supported.", "Error");
                    return null;
            }
        }
    }
}
