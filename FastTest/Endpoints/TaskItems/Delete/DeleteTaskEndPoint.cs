using FastEndpoints;
using FastTest.Application.Commands;
using FastTest.Endpoints.TaskItems.Create;
using FastTest.Endpoints.TaskItems.Get;
using MediatR;

namespace FastTest.Endpoints.TaskItems.Delete
{
    public class DeleteTaskEndpoint : Endpoint<DeleteTaskCommand>
    {
        private readonly IMediator _mediator;


        public DeleteTaskEndpoint(IMediator mediator, ILogger<DeleteTaskEndpoint> logger)
        {
            _mediator = mediator;
            
        }

        public override void Configure()
        {
            Delete("/tasks/{Id}");
            Roles("User"); //jwt

            Summary(s => {
                s.Summary = "Eliminar tarea";
                s.Description = "Borra una tarea  por su ID";
                s.Responses[204] = "Tarea eliminada correctamente";
                s.Responses[404] = "No se encontró la tarea";
            });
        }

        public override async Task HandleAsync(DeleteTaskCommand req, CancellationToken ct)
        {
            // enviamos el comando al handler
            var deleted = await _mediator.Send(req, ct);
            

            if (!deleted)
            {
                await SendNotFoundAsync(ct);  
                return;
            }
           
            await SendNoContentAsync(ct); // eliminado correctamente
        }
    }
}
