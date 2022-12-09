using System;
using System.Windows.Input;

namespace KeyboardHooker
{
    /// <summary>
    /// Определяет сочетание клавиш и вызываемую функцию при нажатии комбинации
    /// </summary>
    internal class GlobalHotKey
    {
        /// <summary>
        /// Наборы системных клавиш
        /// </summary>
        public ModifierKeys Modifier { get; private set; }

        /// <summary>
        /// Значение клавиши
        /// </summary>
        public Key Key { get; private set; }

        /// <summary>
        /// Метод, вызываемый при нажатии комбинации клавиш <see cref="Modifier"/> и <see cref="Key"/>
        /// </summary>
        public Action Callback { get; private set; }

        /// <summary>
        /// Определяет нажата ли комбинация клавиш
        /// </summary>
        public bool Pressed { get; set; }

        /// <summary>
        /// Определяет можно ли вызвать функцию <see cref="Callback"/>
        /// </summary>
        public bool CanExecute { get; set; }

        /// <summary>
        /// Определяет сочетание клавиш, вызываемую функцию при нажатии комбинации и возможность вызова функции
        /// </summary>
        /// <param name="modifier">Комбинация системных клавиш</param>
        /// <param name="key">Клавиша</param>
        /// <param name="callback">Метод, вызываемый при нажатии комбинации клавиш</param>
        /// <param name="canExecute">Определяет можно ли вызвать функцию <paramref name="callback"/> (по-умолчанию <see langword="true"/>)</param>
        public GlobalHotKey(ModifierKeys modifier, Key key, Action callback, bool canExecute = true)
        {
            Modifier = modifier;
            Key = key;
            Callback = callback;
            CanExecute = canExecute;
        }
    }
}