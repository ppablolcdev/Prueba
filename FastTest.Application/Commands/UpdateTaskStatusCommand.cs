using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTest.Application.Commands
{
    public record UpdateTaskStatusCommand(Guid Id, FastTest.Core.FastTest.Domain.TaskStatus Status) : IRequest<bool>;
}
