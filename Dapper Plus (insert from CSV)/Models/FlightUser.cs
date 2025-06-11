using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace Dapper_Plus__insert_from_CSV_.Models
{
    public class FlightUser
    {
        [Ignore]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FlightNumber { get; set; } = string.Empty;
        public string DepartureCity { get; set; } = string.Empty;
        public TimeSpan DepartureTime { get; set; }
        public string ArrivalCity { get; set; } = string.Empty;

    }
}
