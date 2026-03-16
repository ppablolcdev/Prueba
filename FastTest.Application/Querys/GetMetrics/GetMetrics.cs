using FastTest.Infraestructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FastTest.Application.Querys.GetMetrics
{
    public record GetMetricsQuery() : IRequest<MetricsResult>;



    public class GetMetricsQueryHandler : IRequestHandler<GetMetricsQuery, MetricsResult>
    {
        private readonly FastTestDbContext _dbContext;

        public GetMetricsQueryHandler(FastTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MetricsResult> Handle(GetMetricsQuery request, CancellationToken ct)
        {
            var total = await _dbContext.TaskItems.CountAsync(ct);
            var pending = await _dbContext.TaskItems.CountAsync(t => t.Status == Core.FastTest.Domain.TaskStatus.Pending, ct);
            var inProgress = await _dbContext.TaskItems.CountAsync(t => t.Status == Core.FastTest.Domain.TaskStatus.InProgress, ct);
            var completed = await _dbContext.TaskItems.CountAsync(t => t.Status == Core.FastTest.Domain.TaskStatus.Completed, ct);

            return new MetricsResult
            {
                Total = total,
                Pending = pending,
                InProgress = inProgress,
                Completed = completed
            };
        }
    }
}
