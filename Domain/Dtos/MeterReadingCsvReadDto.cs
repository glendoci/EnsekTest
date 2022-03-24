using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class MeterReadingCsvReadDto
    {
        public string AccountId { get; set; } 
        public string MeterReadingDateTime { get; set; }
        public string? MeterReadValue { get; set; }
    }
}
