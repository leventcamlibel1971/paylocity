using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Interfaces
{
    public interface IPaycheckFacade
    {
        /// <summary>
        ///     calculates  the employee payroll
        /// </summary>
        /// <param name="employeeModel">The current employee</param>
        /// <returns></returns>
        PaycheckModel Calculate(EmployeeModel employeeModel);
    }
}