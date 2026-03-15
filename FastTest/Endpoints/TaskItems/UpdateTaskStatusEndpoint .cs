using FastEndpoints;
using FastTest.Application.Commands;
using MediatR;

namespace FastTest.Endpoints.TaskItems
{
    public class UpdateTaskStatusEndpoint : Endpoint<UpdateTaskStatusRequest>
    {

        private readonly IMediator mediator;

        public UpdateTaskStatusEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override void Configure()
        {
            Put("/tasks/{id}");
            Roles("User");
        }

        public override async Task HandleAsync(UpdateTaskStatusRequest req, CancellationToken ct)
        {

            var ok = await mediator.Send(
                new UpdateTaskStatusCommand(req.Id, req.Status), ct);

            if (!ok)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(ct);

        }
    }
}
