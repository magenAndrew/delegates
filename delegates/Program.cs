using  Delegates;
using static Delegates.Dir;

internal class Program
{/// <summary>
 /// Описание/Пошаговая инструкция выполнения домашнего задания:
 /// 1. Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции
 ///    Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности поиска максимального значения.
 ///     public static T GetMax(this IEnumerable collection, Func<T, float> convertToNumber) where T : class;
 /// 2. Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
 /// 3. Оформить событие и его аргументы с использованием.NET соглашений:
 ///    public event EventHandler FileFound; FileArgs – будет содержать имя файла и наследоваться от EventArgs
 /// 4. Добавить возможность отмены дальнейшего поиска из обработчика;
 /// 5. Вывести в консоль сообщения, возникающие при срабатывании событий и результат поиска максимального элемента.
 /// </summary>
 /// <param name="args"></param>
    private static void Main(string[] args)
    {
        var path = (args.Length > 0) ? args[0] : Environment.CurrentDirectory;
        var dir = new Dir(path);

        Console.WriteLine($"Target path: {path}");
        dir.FileFound += SubscriberFileTouched1;

        dir.FileFound += SubscriberFileTouched2;
        var maxFile = dir.MaxFile;
        Console.WriteLine($"Max File is {maxFile.Name}, size : {maxFile.Length} ");
        Console.WriteLine($"Let's start processing files");
        dir.ProcessFile();

    }
    /// <summary>
    /// Пкрвый делегат просто перебирает все файлы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void SubscriberFileTouched1(object sender, FileArgs e)
    {
        Console.WriteLine($"Subscriber 1:Touched the file  {e.Name}");
    }
    /// <summary>
    /// Второй делегат перебирает файлы, пока не находит файл с максимальным размером
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void SubscriberFileTouched2(object sender, FileArgs e)
    {
        if (e.Name.Equals(((Dir)sender)?.MaxFile.Name))
        {

            ((Dir)sender).FileFound -= SubscriberFileTouched2;
            Console.WriteLine("Subscriber 2:It is мax size file! I had stopped work.");
        }
        else {
            Console.WriteLine($"Subscriber 2:This file is less than the maximum size.");

        }
    }
}