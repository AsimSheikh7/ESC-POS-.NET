using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public static class DeviceFile
{
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern SafeFileHandle CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        IntPtr hTemplateFile);

    public static FileStream OpenDevice(string devicePath, FileAccess access)
    {
        const uint OPEN_EXISTING = 3;
        const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        uint desiredAccess = access == FileAccess.Read ? 0x80000000 : 0x40000000; // GENERIC_READ or GENERIC_WRITE

        SafeFileHandle handle = CreateFile(
            devicePath,
            desiredAccess,
            0, // No sharing
            IntPtr.Zero,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL,
            IntPtr.Zero);

        if (handle.IsInvalid)
            throw new IOException("Unable to open device", Marshal.GetLastWin32Error());

        return new FileStream(handle, access);
    }
}
