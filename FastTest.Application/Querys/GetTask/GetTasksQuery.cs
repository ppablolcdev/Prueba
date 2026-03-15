using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FastTest.Application.Querys.GetTask
{
    public record GetTasksQuery(int Page, int PageSize)
    : IRequest<PagedTaskResponse>
    {
    }
}
