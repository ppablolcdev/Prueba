using FastEndpoints;
using FastTest.Application.Commands;
using MediatR;

namespace FastTest.Endpoints.TaskItems
{
    public class CreateTaskEndpoint : Endpoint<CreateTaskRequest, object>
    {

        private readonly IMediator mediator;
        //para logs
        private readonly ILogger<ListTasksEndpoint> _logger;

        public CreateTaskEndpoint(IMediator mediator, ILogger<ListTasksEndpoint> logger)
        {
            this.mediator = mediator;
            _logger=logger;
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

            //Loggear
            _logger.LogInformation("CreateTasks invocado ; valores Title: {Title} Description: {Description}", req.Title, req.Description);

            var id = await mediator.Send(new CreateTaskCommand(req.Title, req.Description), ct);

            await SendAsync(new { id = id }, 201, ct);

        }
    }
}
