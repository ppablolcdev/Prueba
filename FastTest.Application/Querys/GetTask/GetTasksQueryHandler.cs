using FastTest.Application.Commands;
using FastTest.Application.Querys.GetTask;
using FastTest.Infraestructure.Contexts;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, PagedTaskResponse>
{
    private readonly FastTestDbContext _dbContext;
    //para logs
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public GetTasksQueryHandler(FastTestDbContext dbContext, ILogger<CreateTaskCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedTaskResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ListTasks invocado ; valores Page: {Page} PageSize: {PageSize}", request.Page, request.PageSize);

        try
        {
            //  query base
            var query = _dbContext.TaskItems.AsQueryable();

            //  total registros (para paginar)
            var totalCount = await query.CountAsync(cancellationToken);

            // a orden y paginación
            var tasks = await query
                .OrderBy(t => t.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var result = new List<TaskResponse>();

            foreach (var task in tasks)
            {
                result.Add(new TaskResponse
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status.ToString(),
                    CreatedAt = task.CreatedAt,
                    UpdatedAt = task.UpdatedAt
                });
            }

            _logger.LogInformation("ListTasks ejecutado ; valores Page: {Page} PageSize: {PageSize}", request.Page, request.PageSize);

            return new PagedTaskResponse
            {
                TotalCount = totalCount,
                Items = result
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ListTasks exception ; valores  {@Request}, Ex {Message}", request, ex.Message);
            throw;
        }
    }
}
