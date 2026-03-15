using FastTest.Infraestructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Application.Commands
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, bool>
    {

        private readonly FastTestDbContext _db;

        public UpdateTaskStatusCommandHandler(FastTestDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken ct)
        {

            var task = await _db.TaskItems
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (task == null)
                return false;

            // cambio de estado
            task.Status = (Core.FastTest.Domain.TaskStatus)request.Status;

            task.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
