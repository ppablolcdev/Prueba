using FastTest.Core.FastTest.Domain;
using FastTest.Infraestructure.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Application.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly FastTestDbContext _dbContext;
        //para logs
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public CreateTaskCommandHandler(FastTestDbContext dbCotext, ILogger<CreateTaskCommandHandler> logger)
        {
            _dbContext = dbCotext;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken ct)
        {



            // validación simple
            if (string.IsNullOrWhiteSpace(request.Title))
            {

                //Loggear
                _logger.LogInformation("CreateTasks validacion ; validacion: El titulo es requerido");
                throw new Exception("El titulo es requerid");
            }

            //Loggear
            _logger.LogInformation("CreateTasks invocado ; valores Title: {Title} Description: {Description}", request.Title, request.Description);


            try
            {
                var task = new TaskItem
                {
                    Title = request.Title,
                    Description = request.Description,
                    Status = Core.FastTest.Domain.TaskStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.TaskItems.Add(task);

                await _dbContext.SaveChangesAsync(ct);

                //Loggear
                _logger.LogInformation("CreateTasks ejecutado ; valores Title: {Title} Description: {Description}", request.Title, request.Description);
                return task.Id;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteTasks exception ;  valores  {@Request}, Ex {Message}", request, ex.Message);
                throw;
            }

        }
            
    }
}

