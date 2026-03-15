using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Application.Commands
{

    public record DeleteTaskCommand(Guid? Id) : IRequest<bool>;
}
