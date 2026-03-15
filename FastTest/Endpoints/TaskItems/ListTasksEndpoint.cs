using FastEndpoints;
using FastTest.Application.Querys;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;


namespace FastTest.Endpoints.TaskItems
{
    public class ListTasksEndpoint
     : EndpointWithoutRequest<PagedTaskResponse>
    {

        private readonly IMediator mediator;

        public ListTasksEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override void Configure()
        {
            Get("/tasks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            int page = ParsePositiveInt(HttpContext.Request.Headers["X-Page"], 1);
            int pageSize = ParsePositiveInt(HttpContext.Request.Headers["X-PageSize"], 10);

            var result = await mediator.Send(new GetTasksQuery(page, pageSize), ct);

            await SendAsync(result, cancellation: ct);

        }

        private static int ParsePositiveInt(string? value, int defaultValue)
        {
            return int.TryParse(value, out int parsedValue) && parsedValue > 0
                ? parsedValue
                : defaultValue;
        }

    }
}
