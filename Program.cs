using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptoHashCalc
{
    internal static class Program
    {
        // --- Win32 Interop for console handling ---
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// Ensures a console is attached, either by attaching to the parent
        /// process or allocating a new console window if necessary.
        /// </summary>
        private static void EnsureConsole()
        {
            if (!AttachConsole(ATTACH_PARENT_PROCESS))
                AllocConsole();
        }

        [STAThread]
        static int Main(string[] args)
        {
            bool cliMode = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("--cli", StringComparison.OrdinalIgnoreCase))
                {
                    cliMode = true;
                    args[i] = null; // Mark for removal
                }
            }

            args = Array.FindAll(args, a => a != null);

            if (HasArg(args, "--version", "-v"))
            {
                EnsureConsole();
                PrintVersion();
                return 0;
            }

            if (HasArg(args, "--help", "-h"))
            {
                EnsureConsole();
                PrintHelp();
                return 0;
            }

            // --- CLI HASH MODE ---
            if (cliMode) 
            {
                EnsureConsole();
                return RunCliMode(args);
            }
                

            // --- GUI MODE (default) ---
            return RunGuiMode(args);
        }

        /// <summary>
        /// Check if args contains a flag.
        /// </summary>
        private static bool HasArg(string[] args, string longForm, string shortForm)
        {
            foreach (var a in args)
            {
                if (a == null) continue;
                if (a.Equals(longForm, StringComparison.OrdinalIgnoreCase)) return true;
                if (a.Equals(shortForm, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        // ---------------------------------------------------------
        // CLI MODE
        // ---------------------------------------------------------
        private static int RunCliMode(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Missing arguments.\n");
                PrintHelp();
                return 1;
            }

            string filePath = args[0];
            string algorithm = args[1].Trim().ToUpperInvariant();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found: {filePath}");
                return 2;
            }

            string hash = CryptoHashMain.CalcHash(filePath, algorithm);

            if (hash == null)
            {
                Console.WriteLine("Error: Hash computation failed.");
                return 3;
            }

            Console.WriteLine($"{algorithm}({filePath}) = {hash}");
            return 0;
        }

        // ---------------------------------------------------------
        // GUI MODE
        // ---------------------------------------------------------
        private static int RunGuiMode(string[] args)
        {
            if (args.Length < 2)
            {
                MessageBox.Show("Invalid usage!\nUse --help for usage instructions.",
                    "CryptoHashCalc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            string filePath = args[0];
            string algorithm = args[1].Trim().ToUpperInvariant();

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found:\n{filePath}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 2;
            }

            string hash = CryptoHashMain.CalcHash(filePath, algorithm);
            if (hash == null)
            {
                MessageBox.Show("An error occurred while calculating the hash!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 3;
            }

            DisplayRes(filePath, algorithm, hash);
            return 0;
        }

        // ---------------------------------------------------------
        // SHARED UTILS
        // ---------------------------------------------------------

        const string title = "Crypto Hash Calc";

        private static void DisplayRes(string fileName, string algorithm, string hash)
        {
            string message = $"{title}\nFile name: {fileName}\n{algorithm}: {hash}";

            DialogResult result = MessageBox.Show($"{message}\n\nCopy to clipboard?", 
                title, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Clipboard.SetText(message);
        }

        private static void PrintVersion()
        {
            var ver = FileVersionInfo.GetVersionInfo(typeof(Program).
                Assembly.Location).FileVersion;

            Console.WriteLine($"CryptoHashCalc version {ver}");
        }

        private static void PrintHelp()
        {
            Console.WriteLine(title);
            Console.WriteLine("Usage:");
            Console.WriteLine("  CryptoHashCalc.exe <file> <algorithm> [--cli]\n");
            Console.WriteLine("Algorithms:");
            Console.WriteLine("  CRC32, MD5, SHA256, SHA384, SHA512, SHA3-256, SHA3-384, SHA3-512\n");
            Console.WriteLine("Options:");
            Console.WriteLine("  -h, --help       Show this help message");
            Console.WriteLine("  -v, --version    Show version information");
            Console.WriteLine("  --cli            Run in command-line mode (no GUI)");
        }
    }
}
