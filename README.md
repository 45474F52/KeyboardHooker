# KeyboardHooker




## Languages:

- [Русский](#основа-создания-и-настройки-службы)
- [English](#basis-for-creating-and-configuring-a-service)
___
## Основа создания и настройки службы
> Алгоритм взят из [этого видео](https://www.youtube.com/watch?v=8fsoqYSCfZg)
1. Создать проект `Служба Windows (.NET Framework)`
2. ПКМ по конструктору службы - `добавить установщик`
3. В свойствах объекта `serviceProcessInstaller1` установить `Account` в `LocalSystem` (+ указать необходимые настройки для serviceInstaller1)
4. Собрать службу
    1. Собрать решение
    2. В командной строке (от им. адм.) перейти в системную папку с приложением `installUtil`
    3. Прописать `installutil` и путь до недавно собранного решения со службой
---
* Запуск службы: sc start `имя службы`
* Остановка службы: sc stop `имя службы`
* Удаление службы: добавить ключ `/u` к команде из п.4.3

>Код взят из [этого видео](https://www.youtube.com/watch?v=qLxqoh1JLnM)
```C#
Console.WriteLine("Hello World!");
```
<pre>
    <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>L</kbd> - Вызов приложения
</pre>
___
## Basis for creating and configuring a service
