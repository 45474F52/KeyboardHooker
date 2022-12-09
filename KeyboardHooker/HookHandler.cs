using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KeyboardHooker
{
    /// <summary>
    /// Обработчик процедур перехвата нажатий комбинаций клавиш
    /// </summary>
    internal static class HookHandler
    {
        /// <summary>
        /// Инициализирует список горячих клавиш <see cref="GlobalHotKey"/>
        /// </summary>
        static HookHandler()
        {
            GlobalHotKeys = new List<GlobalHotKey>();
        }

        /// <summary>
        /// Список сочетаний клавиш <see cref="GlobalHotKey"/>
        /// </summary>
        private static List<GlobalHotKey> GlobalHotKeys { get; set; }

        /// <summary>
        /// Дескриптор функции перехвата
        /// </summary>
        private static IntPtr _hHook = IntPtr.Zero;

        /// <summary>
        /// Тип перехвата
        /// </summary>
        private static readonly NETHookType _hookType;

        private static readonly PInvoke.HookProc KeyboardHook = HookCallback;

        /// <summary>
        /// Функция обработки перехвата нажатой комбинации клавиш
        /// </summary>
        /// <param name="nCode">Тип устанавливаемой процедуры перехватчика</param>
        /// <param name="wParam">Указывает, отправляется ли сообщение текущим процессом.
        /// Если сообщение отправлено текущим процессом, параметр ненулевой</param>
        /// <param name="lParam"></param>
        /// <returns>Если значение <paramref name="nCode"/> меньше нуля,
        /// процедура должна вернуть значение, возвращаемое <see cref="CallNextHookEx(IntPtr, int, IntPtr, IntPtr)"/></returns>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                foreach (var hotKey in GlobalHotKeys)
                {
                    if (Keyboard.Modifiers == hotKey.Modifier && Keyboard.IsKeyDown(hotKey.Key) && !hotKey.Pressed)
                    {
                        if (hotKey.CanExecute)
                        {
                            hotKey.Callback?.Invoke();
                            hotKey.Pressed = true;
                        }
                    }

                    if (hotKey.Pressed && Keyboard.IsKeyUp(hotKey.Key))
                    {
                        hotKey.Pressed = false;
                    }
                }
            }

            return PInvoke.CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// Запускает процедуру перехвата
        /// </summary>
        /// <param name="hookProc">Процедура перехвата</param>
        /// <returns>Возвращает данные из метода <see cref="SetHookEx(int, HookProc, IntPtr, int)"/></returns>
        private static IntPtr SetHook(PInvoke.HookProc hookProc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    IntPtr hookProcPtr = PInvoke.SetHookEx((int)_hookType, hookProc, PInvoke.GetModuleHandle(curModule.ModuleName), 0u);
                    ErrorHandling();
                    return hookProcPtr;
                }
            }
        }

        private static bool _consoleAlloc;
        public static bool IsHookSetup { get; private set; }

        /// <summary>
        /// Запускает перехватчик
        /// </summary>
        public static void SetupHook()
        {
            if (!IsHookSetup)
            {
                _hHook = SetHook(KeyboardHook);
                IsHookSetup = true;
                ErrorHandling();
            }
        }

        private static void ErrorHandling()
        {
            int error = Marshal.GetLastWin32Error();
            if (error != 0)
            {
                if (!_consoleAlloc)
                {
                    _ = PInvoke.AllocConsole();
                    _consoleAlloc = true;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"\nВозникла ошибка WinAPI №{error}!");
                Console.WriteLine("Нажмите любую клавишу для выхода из консоли");
                Console.ReadLine();

                _ = PInvoke.FreeConsole();
                _consoleAlloc = false;
            }
        }

        /// <summary>
        /// Удаляет перехватчик
        /// </summary>
        public static void ShutdownHook()
        {
            if (IsHookSetup)
            {
                PInvoke.UnsetHookEx(_hHook);
                IsHookSetup = false;
            }
        }

        /// <summary>
        /// Добавляет новое сочетание клавиш <see cref="GlobalHotKey"/> в список, который будет обрабатываться перехватчиком
        /// </summary>
        /// <param name="hotKey">Сочетание клавиш</param>
        public static void AddHotKey(GlobalHotKey hotKey) => GlobalHotKeys.Add(hotKey);

        /// <summary>
        /// Удаляет сочетание клавиш <see cref="GlobalHotKey"/> из списка, который будет обрабатываться перехватчиком
        /// </summary>
        /// <param name="hotKey">Сочетание клавиш</param>
        public static void RemoveHotKey(GlobalHotKey hotKey) => GlobalHotKeys.Remove(hotKey);
    }
}