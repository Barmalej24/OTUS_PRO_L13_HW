using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace OTUS_PRO_L13_HW.Test
{
    /// <summary>
    /// Класс для тестирования JSON
    /// </summary>
    public class JsonTest
    {
        public JsonTest() { }

        /// <summary>
        /// Старт тестирования
        /// </summary>
        public void Start(int iterator, bool print = true)
        {
            var sb = new StringBuilder();
            var sw = new Stopwatch();
            int count = 0;

            sw.Start();
            while (count < iterator)
            {
                var stringJson = Serialize();
                sb.AppendLine($"Json => {stringJson}");
                count++;
            }
            sw.Stop();

            if (print)
            {
                Print(sb.ToString());
            }

            Console.WriteLine($"Время сериализации в JSON {sw.Elapsed.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        private string Serialize()
        {
            var tf = F.Get();
            return JsonSerializer.Serialize(tf);
        }

        /// <summary>
        /// Печать
        /// </summary>
        private void Print(string json)
        {
            var sw = new Stopwatch();
            sw.Restart();
            Console.WriteLine(json);
            sw.Stop();
            Console.WriteLine($"Время печати JSON {sw.Elapsed.TotalMilliseconds} ms");
        }
    }
}
