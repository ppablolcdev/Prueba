using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Application.Querys.GetMetrics
{
    public class MetricsResult
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
    }
}
