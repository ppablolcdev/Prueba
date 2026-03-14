using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

public class HelloEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/hello");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync("Hello from FastEndpoints!");
    }
}