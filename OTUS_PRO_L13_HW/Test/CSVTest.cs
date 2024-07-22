using OTUS_PRO_L13_HW.CSV;
using System.Diagnostics;
using System.Text;

namespace OTUS_PRO_L13_HW.Test
{
    /// <summary>
    /// Класс для тестирования CSV
    /// </summary>
    public class CSVTest : ISrializable<F>
    {
        public CSVTest() { }

        /// <summary>
        /// Старт тестирования CSV
        /// </summary>
        public void Strat(int iterator, bool print = true)
        {
            var sb = new StringBuilder();
            var sw = new Stopwatch();
            int count = 0;
            var path = "csv.csv";

            sw.Start();
            while (count < iterator)
            {
                var csvString = Serialize(F.Get());
                sb.AppendLine($"CSV => {csvString}");
                count++;
            }
            sw.Stop();

            if (print)
            {
                Print(sb.ToString());
            }

            Console.WriteLine($"Время сериализации CSV {sw.Elapsed.TotalMilliseconds} ms");

            SerializeSave(F.Get(), path);

            count = 0;
            sw.Restart();
            while (count < iterator)
            {
                Deserialize(path);
                count++;
            }
            sw.Stop();

            Console.WriteLine($"Время десериализации CSV {sw.Elapsed.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        public string Serialize(F entity)
        {
            var csv = new CSVSerializator<F>();
            var csvString = csv.Serialize(entity);

            return csvString;
        }
        /// <summary>
        /// Сериализация в файл
        /// </summary>
        private void SerializeSave(F entity, string path)
        {
            var csv = new CSVSerializator<F>();
            using var streamCreate = new FileStream(path, FileMode.Create, FileAccess.Write);
            csv.SerializeSave(streamCreate, entity);
        }

        /// <summary>
        /// Десериализация в коллекцию
        /// </summary>
        public IEnumerable<F> DeserializeCollection(string path)
        {
            var csv = new CSVSerializator<F>();
            using var streamRead = new FileStream(path, FileMode.Open, FileAccess.Read);
            return csv.Deserialize(streamRead);
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        public F Deserialize(string path)
        {
            var csv = new CSVSerializator<F>();
            using var streamRead = new FileStream(path, FileMode.Open, FileAccess.Read);
            return csv.Deserialize(streamRead).FirstOrDefault();
        }

        /// <summary>
        /// Печать CSV
        /// </summary>
        private void Print(string json)
        {
            var sw = new Stopwatch();
            sw.Restart();
            Console.WriteLine(json);
            sw.Stop();
            Console.WriteLine($"Время печати CSV {sw.Elapsed.TotalMilliseconds} ms");
        }

    }
}
