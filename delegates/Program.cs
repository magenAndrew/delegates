using  Delegates;
using static Delegates.Dir;

internal class Program
{
    private static void Main(string[] args)
    {
        var path = (args.Length > 0) ? args[0] : Environment.CurrentDirectory;
        var dir = new Dir(path);

        Console.WriteLine($"Target path: {path}");
        dir.FileFound += subscriberFileTouched1;

        dir.FileFound += subscriberFileTouched2;
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
    static void subscriberFileTouched1(object sender, FileArgs e)
    {
        Console.WriteLine($"Subscriber 1:Touched the file  {e.Name}");
    }
    /// <summary>
    /// Второй делегат перебирает файлы, пока не находит файл с максимальным размером
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void subscriberFileTouched2(object sender, FileArgs e)
    {
        if (e.Name.Equals(((Dir)sender)?.MaxFile.Name))
        {

            ((Dir)sender).FileFound -= subscriberFileTouched2;
            Console.WriteLine("Subscriber 2:It is мax size file ! I had stopped work.");
        }
        else {
            Console.WriteLine($"Subscriber 2:This file is less than the maximum size.");

        }
    }
}