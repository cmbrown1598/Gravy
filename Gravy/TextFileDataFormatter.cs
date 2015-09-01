using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gravy
{
    public class TextFileDataFormatter<T>
    {
        public IEnumerable<T> Data { get; set; }
        public Stream Stream { get; set; }

        public Rule FormattingRule { get; set; }

        public void Format()
        {
            var writer = new StreamWriter(Stream);

            foreach (var result in Data.Select(item => FormattingRule.Apply<T>(item)))
            {
                writer.WriteLine(result);
            }

            writer.Flush();
            writer = null;
        }
    }
}