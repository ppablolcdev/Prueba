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
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly FastTestDbContext _bdContext;
        //para logs
        private readonly ILogger<DeleteTaskCommandHandler> _logger;

        public DeleteTaskCommandHandler(FastTestDbContext dbContext , ILogger<DeleteTaskCommandHandler> logger)
        {
            _bdContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            //Loggear llamada
            _logger.LogInformation("DeleteTasks invocado ; valores ID: {ID}", request.Id);

            try
            {
                // buscar  tarea
                var task = await _bdContext.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);

                if (task == null)
                {
                    //Loggear Error
                    _logger.LogInformation("DeleteTasks no realizado ; valores ID: {ID}", request.Id);
                    return false; // no existe
                }



                _bdContext.TaskItems.Remove(task);

                await _bdContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteTasks exception ; valores ID: {ID}, Ex {Message}", request.Id, ex.Message);
                throw;
            }

            //Loggear Respuesta
            _logger.LogInformation("DeleteTasks ejecutado ; valores ID: {ID}", request.Id);
            return true; // eliminada
        }
    }
}
