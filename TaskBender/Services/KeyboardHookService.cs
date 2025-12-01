using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TaskBender.Services
{
    public class KeyboardHookService : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private readonly SettingsService _settingsService;

        public event EventHandler? OnSpotlightTriggered;
        public event EventHandler? OnQuickLookShow;
        public event EventHandler? OnQuickLookHide;

        public KeyboardHookService(SettingsService settingsService)
        {
            _settingsService = settingsService;
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule? curModule = curProcess.MainModule)
            {
                if (curModule == null) return IntPtr.Zero;
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == _settingsService.Settings.SpotlightKey) // Spotlight
                {
                    if (wParam == (IntPtr)WM_KEYDOWN)
                    {
                        OnSpotlightTriggered?.Invoke(this, EventArgs.Empty);
                        return (IntPtr)1; // Suppress key
                    }
                    if (wParam == (IntPtr)WM_KEYUP)
                    {
                        return (IntPtr)1; // Suppress key
                    }
                }
                else if (vkCode == _settingsService.Settings.QuickLookKey) // QuickLook
                {
                    if (wParam == (IntPtr)WM_KEYDOWN)
                    {
                        OnQuickLookShow?.Invoke(this, EventArgs.Empty);
                        return (IntPtr)1; // Suppress key
                    }
                    else if (wParam == (IntPtr)WM_KEYUP)
                    {
                        OnQuickLookHide?.Invoke(this, EventArgs.Empty);
                        return (IntPtr)1; // Suppress key
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookID);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
