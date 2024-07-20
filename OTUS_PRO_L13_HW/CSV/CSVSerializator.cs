using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace OTUS_PRO_L13_HW.CSV
{
    public class CSVSerializator<T> where T : class, new()
    {
        private readonly List<PropertyInfo> _properties;
        private readonly string _separator = ",";
        public CSVSerializator()
        {

            var type = typeof(T);

            var properties = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty);

            _properties = properties.ToList();
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="stream">поток</param>
        /// <returns>Класс</returns>
        public ICollection<T> Deserialize(Stream stream)
        {
            string[] columns;
            string[] rows;
            var dataTemp = new T();
            var result = new List<T>();

            try
            {
                using var sr = new StreamReader(stream);
                columns = sr.ReadLine().Split(_separator);
                rows = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            }
            catch (Exception ex)
            {
                throw new InvalidCsvException(ex);
            }            

            for (int row = 0; row < rows.Length; row++)
            {
                    var line = rows[row];
                    var parts = line.Split(_separator);

                for (int i = 0; i < parts.Length; i++)
                {
                    var value = parts[i].Trim();
                    var column = columns[i];

                    var p = _properties.FirstOrDefault(a => a.Name.Equals(column, StringComparison.InvariantCultureIgnoreCase));

                    if (value.IndexOf("\"") == 0)
                        value = value.Substring(1);

                    if (value[value.Length - 1].ToString() == "\"")
                        value = value.Substring(0, value.Length - 1);

                    var converter = TypeDescriptor.GetConverter(p.PropertyType);
                    var convertedvalue = converter.ConvertFrom(value);

                    p.SetValue(dataTemp, convertedvalue);
                }

                result.Add(dataTemp);
            }

            return result;
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>строка</returns>
        public string Serialize(T data)
        {
            var sb = new StringBuilder();
            var values = new List<string>();
            sb.AppendLine(GetHeader());

            foreach (var p in _properties)
            {
                var raw = p.GetValue(data);
                var value = raw == null ? "" :
                    raw.ToString()?
                    .Replace(_separator, ((char)0x255).ToString())
                    .Replace(Environment.NewLine, ((char)0x254).ToString());

                value = string.Format("\"{0}\"", value);

                values.Add(value);
            }
            sb.AppendLine(string.Join(_separator, values));

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Сериализация в файл
        /// </summary>
        /// <param name="stream">Поток</param>
        /// <param name="data">Данные</param>
        public void SerializeSave(Stream stream, T data)
        {
            var stringjson = Serialize(data);

            using var sw = new StreamWriter(stream);
            sw.Write(stringjson.Trim());

        }

        /// <summary>
        /// Получение заголовков
        /// </summary>
        /// <returns>Строка</returns>
        private string GetHeader()
        {
            var header = _properties.Select(a => a.Name);
            return string.Join(_separator, header.ToArray());
        }
    }
}
