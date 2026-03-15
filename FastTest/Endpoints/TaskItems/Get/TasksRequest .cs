using FastEndpoints;

namespace FastTest.Endpoints.TaskItems.Get
{
    public class TasksRequest
    {
        [FromHeader("X-Page")] public int Page { get; set; } = 1;
        [FromHeader("X-PageSize")] public int PageSize { get; set; } = 10;
    }
}
