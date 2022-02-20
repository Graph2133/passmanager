using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace PassManager.Console.Clipboard
{
    public static class WindowsClipboard
    {
        private const uint UNICODE = 13;

        public static void SetText(string text)
        {
            OpenClipboard();
            SetClipboardText(text);
        }

        private static void OpenClipboard()
        {
            var retryCount = 5;
            while (true)
            {
                if (OpenClipboard(default))
                {
                    break;
                }

                if (--retryCount == 0)
                {
                    ThrowWin32Exception();
                }

                Thread.Sleep(50);
            }
        }

        private static void SetClipboardText(string text)
        {
            IntPtr hGlobal = default;
            try
            {
                EmptyClipboard();

                var allocSize = (text.Length + 1) * 2;
                hGlobal = Marshal.AllocHGlobal(allocSize);
                if (hGlobal == default)
                {
                    ThrowWin32Exception();
                }

                var target = GlobalLock(hGlobal);
                if (target == default)
                {
                    ThrowWin32Exception();
                }

                try
                {
                    Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
                }
                finally
                {
                    GlobalUnlock(target);
                }

                if (SetClipboardData(UNICODE, hGlobal) == default)
                {
                    ThrowWin32Exception();
                }

                hGlobal = default;
            }
            finally
            {
                if (hGlobal != default)
                {
                    Marshal.FreeHGlobal(hGlobal);
                }

                CloseClipboard();
            }
        }

        private static void ThrowWin32Exception()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();
    }
}
