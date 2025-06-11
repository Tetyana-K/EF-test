using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Plus__insert_from_CSV_
{
    public class TimeSpanConverter : CsvHelper.TypeConversion.DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (TimeSpan.TryParse(text, out var result))
                return result;

            // Альтернатива для формату HH:mm
            if (TimeOnly.TryParse(text, out var t))
                return new TimeSpan(t.Hour, t.Minute, 0);

            return TimeSpan.Zero;
        }
    }

}
