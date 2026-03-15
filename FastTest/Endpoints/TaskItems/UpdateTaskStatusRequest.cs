namespace FastTest.Endpoints.TaskItems
{
    public class UpdateTaskStatusRequest
    {
        public Guid Id { get; set; }
        public FastTest.Core.FastTest.Domain.TaskStatus Status { get; set; }
    }
}
