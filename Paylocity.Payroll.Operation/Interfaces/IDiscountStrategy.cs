using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Interfaces
{
    public interface IDiscountStrategy
    {
        /// <summary>
        ///     defines the discount strategy
        /// </summary>
        /// <param name="payrollConfig">Employee Payroll Config</param>
        /// <param name="employee">The current employee</param>
        /// <returns></returns>
        decimal Apply(PayrollConfigModel payrollConfig, EmployeeModel employee);
    }
}