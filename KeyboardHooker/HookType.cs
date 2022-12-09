using System;

namespace KeyboardHooker
{
    /// <summary>
    /// Типы перехвата
    /// </summary>
    [Obsolete("Не все типы поддерживаются в .NET Framework. Исп. NETHookType")]
    internal enum HookType : int
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    /// <summary>
    /// Типы перехвата, поддерживаемые в .NetFramework
    /// </summary>
    internal enum NETHookType : int
    {
        /// <summary>
        /// Устанавливает процедуру перехватчика, которая отслеживает низкоуровневые события ввода с клавиатуры
        /// </summary>
        WH_KEYBOARD_LL = 13,
        /// <summary>
        /// Устанавливает процедуру перехватчика, которая отслеживает низкоуровневые события ввода мыши
        /// </summary>
        WH_MOUSE_LL = 14
    }
}