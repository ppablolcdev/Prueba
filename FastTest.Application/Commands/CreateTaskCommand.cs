using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
namespace FastTest.Application.Commands
{
    public record CreateTaskCommand(string Title, string? Description) : IRequest<Guid>;
}
