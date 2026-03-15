using FastEndpoints;
using FastTest.Application.Commands;
using FastTest.Endpoints.TaskItems.Get;
using MediatR;

namespace FastTest.Endpoints.TaskItems.Create
{
    public class CreateTaskEndpoint : Endpoint<CreateTaskRequest, object>
    {

        private readonly IMediator mediator;
       

        public CreateTaskEndpoint(IMediator mediator, ILogger<CreateTaskEndpoint> logger)
        {
            this.mediator = mediator;

        }

        public override void Configure()
        {
            Post("/tasks");
            Roles("User"); //jwt
        }

        public override async Task HandleAsync(CreateTaskRequest req, CancellationToken ct)
        {

            //  evitar textos enormes
            if (req.Title?.Length > 200)
                ThrowError("El Titulo es demasiado largo");


            var id = await mediator.Send(new CreateTaskCommand(req.Title, req.Description), ct);

            await SendAsync(new { id }, 201, ct);

        }
    }
}
