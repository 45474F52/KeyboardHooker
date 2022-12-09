using System;
using System.Runtime.InteropServices;

namespace KeyboardHooker
{
    /// <summary>
    /// Предоставляет методы из WinAPI
    /// </summary>
    internal static class PInvoke
    {
        /// <summary>
        /// Делегат процедуры перехвата
        /// </summary>
        /// <param name="nCode">Тип устанавливаемой процедуры перехватчика</param>
        /// <param name="wParam">Указывает, отправляется ли сообщение текущим процессом.
        /// Если сообщение отправлено текущим процессом, параметр ненулевой</param>
        /// <param name="lParam"></param>
        /// <returns>Идентификатор перехватчика</returns>
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Функция установки перехвата
        /// </summary>
        /// <param name="idHook">Тип устанавливаемой процедуры перехватчика</param>
        /// <param name="lpfn">Указатель на процедуру перехватчика.
        /// Если параметр = 0 или указывает на идентификатор потока, созданного другим процессом,
        /// параметр должен указывать на процедуру перехватчика в библиотеке DLL.
        /// В противном случае параметр может указывать на процедуру перехватчика в коде, связанным с текущим процессом.</param>
        /// <param name="hMod">Дескриптор библиотеки DLL, содержащей процедуру перехвата, на которую указывает параметр <paramref name="lpfn"/>.
        /// Параметр должен иметь значение, если параметр <paramref name="threadId"/> указывает поток, созданный текущим процессом,
        /// и если процедура перехвата находится в коде, связанном с текущим процессом.</param>
        /// <param name="threadId">Указывает на идентификатор потока, с которым должна быть связана процедура перехвата.
        /// Если этот параметр = 0, процедура перехвата связывается со всеми существующими потоками,
        /// запущенными в той же рабочей среде, что и вызывающий поток</param>
        /// <returns>Если функция завершается успешно, возвращается дескриптор процедуры обработчика. Иначе возвращается <see langword="null"/></returns>
        [DllImport(
            "user32.dll",
            SetLastError = true,
            EntryPoint = "SetWindowsHookExW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        public static extern IntPtr SetHookEx(
            [In][param: MarshalAs(UnmanagedType.I4)] int idHook,
            [In] HookProc lpfn,
            [In] IntPtr hMod,
            [In][param: MarshalAs(UnmanagedType.U4)] uint threadId);

        /// <summary>
        /// Функция удаления перехвата
        /// </summary>
        /// <param name="hHook">Идентификатор функции перехвата</param>
        /// <returns>Если функция завершилась с ошибкой - возвращается <see langword="null"/></returns>
        [DllImport(
            "user32.dll",
            SetLastError = true,
            EntryPoint = "UnhookWindowsHookEx",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnsetHookEx([In] IntPtr hHook);

        /// <summary>
        /// Функция передачи информации о перехвате следующей процедуре перехвата в цепочке
        /// </summary>
        /// <param name="hHook">Идентификатор функции перехвата</param>
        /// <param name="nCode">Код перехвата передается текущей процедуре перехвата.
        /// Следующая процедура перехвата использует этот код, чтобы определить, как обрабатывать информацию о перехвате.</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>Значение, возвращаемое следующей HOOK-процедурой в цепи и зависит от типа перехвата</returns>
        [DllImport(
            "user32.dll",
            SetLastError = true,
            EntryPoint = "CallNextHookEx",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        public static extern IntPtr CallNextHookEx(
            [In] IntPtr hHook,
            [In][param: MarshalAs(UnmanagedType.I4)] int nCode,
            [In] IntPtr wParam,
            [In] IntPtr lParam);

        /// <summary>
        /// Извлекает дескриптор модуля для <paramref name="lpModuleName"/>. Модуль должен быть загружен вызывающим процессом.
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns>Возвращает дескриптор модуля, если функция выполнилась без ошибок. Иначе - <see langword="null"/></returns>
        [DllImport(
            "kernel32.dll",
            SetLastError = true,
            EntryPoint = "GetModuleHandleW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        public static extern IntPtr GetModuleHandle([In][param: MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

        /// <summary>
        /// Вызывает консоль (для вывода ошибки)
        /// </summary>
        /// <returns>Если функция завершилась с ошибкой - возвращается <see langword="null"/></returns>
        [DllImport(
            "kernel32.dll",
            SetLastError = true,
            EntryPoint = "AllocConsole",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        /// <summary>
        /// Закрывает консоль
        /// </summary>
        /// <returns>Если функция завершилась с ошибкой - возвращается <see langword="null"/></returns>
        [DllImport(
            "kernel32.dll",
            SetLastError = true,
            EntryPoint = "FreeConsole",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeConsole();
    }
}