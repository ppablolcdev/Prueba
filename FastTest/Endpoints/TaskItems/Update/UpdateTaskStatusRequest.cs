namespace FastTest.Endpoints.TaskItems.Update
{
    public class UpdateTaskStatusRequest
    {
        public Guid Id { get; set; }
        public Core.FastTest.Domain.TaskStatus Status { get; set; }
    }
}
