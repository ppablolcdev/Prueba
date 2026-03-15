using FastTest.Application.Querys;
using FastTest.Endpoints.TaskItems;
using FastTest.Infraestructure.Contexts;

using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, PagedTaskResponse>
{
    private readonly FastTestDbContext _dbContext;

    public GetTasksQueryHandler(FastTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedTaskResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
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

        //armar respuesta
        return new PagedTaskResponse
        {
            TotalCount = totalCount,
            Items = result
        };
    }
}
