using System;
using System.Runtime.InteropServices;

namespace BookRP;

public static partial class NativeMethods
{
    private const string User32 = "user32.dll";
    
    [LibraryImport(User32,  SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetForegroundWindow(IntPtr windowHandle);
    
    [LibraryImport(User32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ShowWindow(IntPtr windowHandle, int show);
}