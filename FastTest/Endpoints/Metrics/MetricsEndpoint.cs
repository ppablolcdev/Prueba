using FastEndpoints;
using FastTest.Application.Querys.GetMetrics;
using MediatR;

namespace FastTest.Endpoints.Metrics
{
    public class MetricsEndpoint : EndpointWithoutRequest<MetricsResult>
    {
        private readonly IMediator _mediator;

        public MetricsEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/metrics");
            Roles("User");             Summary(s =>
            {
                s.Summary = "Metricas tareas";
                s.Description = "Clasficacion de tares por estados";
                s.Responses[200] = "MEtricas Ejecutadas";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMetricsQuery(), ct);
            await SendAsync(result, cancellation: ct);
        }
    }
}
