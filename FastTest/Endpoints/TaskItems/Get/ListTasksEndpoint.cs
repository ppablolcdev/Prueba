using FastEndpoints;
using FastTest.Application.Querys.GetTask;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Forms;
using System;


namespace FastTest.Endpoints.TaskItems.Get
{
    public class ListTasksEndpoint : Endpoint<TasksRequest, PagedTaskResponse> 
    {

        private readonly IMediator mediator;


        public ListTasksEndpoint(IMediator mediator, ILogger<ListTasksEndpoint> logger  )
        {
            this.mediator = mediator;
           
        }

        public override void Configure()
        {
            Get("/tasks");
            Roles("User");
            Options(x => x.RequireRateLimiting("api"));

        
        }

        public override async Task HandleAsync(TasksRequest req, CancellationToken ct)
        {

            //probar error global
            //if (1 == 1)
            //{
            //    throw new Exception("error de prueba");
            //}

            //Limitar a valores  que no sean muy grandes
            req.Page = Math.Max(req.Page, 1);
            req.PageSize = Math.Min(req.PageSize, 100);

            var result = await mediator.Send(new GetTasksQuery(req.Page, req.PageSize), ct);

            await SendAsync(result, cancellation: ct);

        }


    }
}
