using System;
using System.IO;
using System.Runtime.InteropServices;
using Serilog;

namespace Api
{
    internal class Utilities
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        public static void LoadNativeAssemblies(string rootApplicationPath)
        {
            var nativeBinaryPath = Path.Combine(rootApplicationPath, @"Ugly\x64\");

            LoadNativeAssembly(nativeBinaryPath, "ucrtbased.dll");
            LoadNativeAssembly(nativeBinaryPath, "vcruntime140d.dll");
            LoadNativeAssembly(nativeBinaryPath, "Ugly.dll");
        }

        private static void LoadNativeAssembly(string nativeBinaryPath, string assemblyName)
        {
            Log.Information("Loading {AssemblyName} from {NativeAssemblyPath}", assemblyName, nativeBinaryPath);

            var path = Path.Combine(nativeBinaryPath, assemblyName);
            var ptr = LoadLibrary(path);
            if (ptr == IntPtr.Zero)
            {
                throw new InvalidOperationException(string.Format(
                    "Error loading {0} (ErrorCode: {1})",
                    assemblyName,
                    Marshal.GetLastWin32Error()));
            }
        }
    }
}