using FastTest.Infraestructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        //para logs
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public UpdateTaskStatusCommandHandler(FastTestDbContext db, ILogger<CreateTaskCommandHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken ct)
        {
            //Loggear
            _logger.LogInformation("UpdateTasks invocado ;  valores  {@Request}", request);


            try
            {
                var task = await _db.TaskItems
                       .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

                if (task == null)
                    return false;

                // cambio de estado
                task.Status = (Core.FastTest.Domain.TaskStatus)request.Status;

                task.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateTasks exception ; valores  {@Request}, Ex {Message}", request, ex.Message);
                throw;
            }

            _logger.LogInformation("UpdateTasks ejecutado ; valores  {@Request}", request);
            return true;
        }
    }
}
