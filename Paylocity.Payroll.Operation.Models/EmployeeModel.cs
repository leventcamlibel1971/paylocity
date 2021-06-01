using System.Collections.Generic;
using FluentValidation;

namespace Paylocity.Payroll.Operation.Models
{
    public class EmployeeModel : PersonModel
    {
        public EmployeeModel()
        {
            Dependents = new List<PersonModel>();
        }

        public ICollection<PersonModel> Dependents { get; set; }
    }

    public class EmployeeModelValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeModelValidator()
        {
            RuleFor(x => x.Name).Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Employee's Name cannot be empty.");
            RuleForEach(x => x.Dependents)
                .Must(t => !string.IsNullOrWhiteSpace(t.Name)).WithMessage("Dependent's Name cannot be empty.");
        }
    }
}