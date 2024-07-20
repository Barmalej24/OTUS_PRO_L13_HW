using OTUS_PRO_L13_HW.CSV;
using System.Diagnostics;
using System.Text;

namespace OTUS_PRO_L13_HW.Test
{
    /// <summary>
    /// Класс для тестирования CSV
    /// </summary>
    public class CSVTest
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

            sw.Start();
            while (count < iterator)
            {
                var csvString = Serialize();
                sb.AppendLine($"CSV => {csvString}");
                count++;
            }
            sw.Stop();

            if (print)
            {
                Print(sb.ToString());
            }

            Console.WriteLine($"Время сериализации CSV {sw.Elapsed.TotalMilliseconds} ms");

            SerializeSave();

            count = 0;
            sw.Restart();
            while (count < iterator)
            {
                Deserialize();
                count++;
            }
            sw.Stop();

            Console.WriteLine($"Время десериализации CSV {sw.Elapsed.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        private string Serialize()
        {
            var csv = new CSVSerializator<F>();
            var tf = F.Get();
            var csvString = csv.Serialize(tf);

            return csvString;
        }
        /// <summary>
        /// Сериализация в файл
        /// </summary>
        private void SerializeSave()
        {
            var csv = new CSVSerializator<F>();
            var tf = F.Get();
            var csvString = csv.Serialize(tf);
            using var streamCreate = new FileStream("csv.csv", FileMode.Create, FileAccess.Write);
            csv.SerializeSave(streamCreate, tf);
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        private void Deserialize()
        {
            var csv = new CSVSerializator<F>();
            using var streamRead = new FileStream("csv.csv", FileMode.Open, FileAccess.Read);
            var classesF = csv.Deserialize(streamRead);
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
