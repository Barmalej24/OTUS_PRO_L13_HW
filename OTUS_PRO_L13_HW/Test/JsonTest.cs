using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace OTUS_PRO_L13_HW.Test
{
    /// <summary>
    /// Класс для тестирования JSON
    /// </summary>
    public class JsonTest : ISrializable<F>
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
                var stringJson = Serialize(F.Get());
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
        public string Serialize(F entity)
        {
            return JsonSerializer.Serialize(entity);
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        public F Deserialize(string path)
        {           
            using var streamRead = new FileStream(path, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(streamRead);

            return JsonSerializer.Deserialize<F>(reader.ReadToEnd());
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
