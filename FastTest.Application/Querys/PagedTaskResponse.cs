using FastTest.Endpoints.TaskItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Application.Querys
{
    public class PagedTaskResponse
    {
        public int TotalCount { get; set; }
        public List<TaskResponse> Items { get; set; } = new();
    }
}
