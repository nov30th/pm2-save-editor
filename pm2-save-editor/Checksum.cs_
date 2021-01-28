using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace pm2_save_editor
{
    /// <summary>
    /// Provides functionality for calcuating checksums of save files using unported C++ code
    /// </summary>
    static class Checksum
    {
        [DllImport("pm2-checksum.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalculateChecksum(IntPtr save_file);

        public static int CalculateChecksum(byte[] file)
        {
            // May want to wrap this in a try/catch/finally block to cover edge cases
            IntPtr fileBuffer = Marshal.AllocHGlobal(file.Length);
            Marshal.Copy(file, 0, fileBuffer, file.Length);

            int checksum = CalculateChecksum(fileBuffer);

            Marshal.FreeHGlobal(fileBuffer);

            return checksum;

        }
    }
}
