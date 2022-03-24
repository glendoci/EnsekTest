using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class ResultDto
    {
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}