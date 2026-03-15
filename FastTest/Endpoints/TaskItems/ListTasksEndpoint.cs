using FastEndpoints;
using FastTest.Application.Querys;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Forms;
using System;


namespace FastTest.Endpoints.TaskItems
{
    public class ListTasksEndpoint
     : EndpointWithoutRequest<PagedTaskResponse>
    {

        private readonly IMediator mediator;
        //para logs
        private readonly ILogger<ListTasksEndpoint> _logger;

        public ListTasksEndpoint(IMediator mediator, ILogger<ListTasksEndpoint> logger  )
        {
            this.mediator = mediator;
            _logger=logger;
        }

        public override void Configure()
        {
            Get("/tasks");
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
            Options(x => x.RequireRateLimiting("api"));
        
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
           


            int page = ParsePositiveInt(HttpContext.Request.Headers["X-Page"], 1);
            int pageSize = ParsePositiveInt(HttpContext.Request.Headers["X-PageSize"], 10);

            //Loggear
            _logger.LogInformation("ListTasks invocado ; valores Page: {Page} PageSize: {PageSize}", page, pageSize);

            //probar error global
            //if (1 == 1)
            //{
            //    throw new Exception("error de prueba");
            //}

            //validar que no se envien valores muy grandes
            page = Math.Max(page, 1);
            pageSize = Math.Min(pageSize, 100);

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
