using OTUS_PRO_L13_HW.Test;

namespace OTUS_PRO_L13_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ITERATION = 100_000;

            Console.WriteLine($"Нажми любую клавишу для начала тестирования стандартной Сериализации JSON. Количество повторений :: {ITERATION}");
            Console.ReadKey();

            var json = new JsonTest();
            json.Start(ITERATION);

            Console.WriteLine($"Нажми любую клавишу для начала тестирования кастомной Сериализации CSV. Количество повторений :: {ITERATION}");
            Console.ReadKey();

            var csv = new CSVTest();
            csv.Strat(ITERATION);
        }
    }
}
