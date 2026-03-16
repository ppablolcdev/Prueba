using FastEndpoints;
using FluentValidation;

namespace FastTest.Endpoints.TaskItems.Update
{
    public class UpdateTaskStatusValidator : Validator<UpdateTaskStatusRequest>
    {
        public UpdateTaskStatusValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
