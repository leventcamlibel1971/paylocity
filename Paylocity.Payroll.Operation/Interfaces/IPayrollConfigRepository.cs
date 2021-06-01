using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Interfaces
{
    public interface IPayrollConfigRepository
    {
        /// <summary>
        ///     returns employee payroll config
        /// </summary>
        /// <returns></returns>
        PayrollConfigModel GetPayrollConfig();
    }
}