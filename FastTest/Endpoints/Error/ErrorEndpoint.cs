using FastEndpoints;

namespace FastTest.Endpoints.Error
{
    public class ErrorEndpoint : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/error");
            AllowAnonymous();
            //quitar del swagger
            Description(x => x.ExcludeFromDescription());
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendAsync(new { message = "Ha ocurrido un error" }, 500);
        }
    }
}
