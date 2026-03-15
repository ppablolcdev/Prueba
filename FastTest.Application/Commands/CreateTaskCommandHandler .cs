using FastTest.Core.FastTest.Domain;
using FastTest.Infraestructure.Contexts;
using MediatR;
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

        public CreateTaskCommandHandler(FastTestDbContext dbCotext)
        {
            _dbContext = dbCotext;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken ct)
        {

            // validación simple
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new Exception("title is required");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = Core.FastTest.Domain.TaskStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.TaskItems.Add(task);

            await _dbContext.SaveChangesAsync(ct);

            return task.Id;
        }
    }
}

