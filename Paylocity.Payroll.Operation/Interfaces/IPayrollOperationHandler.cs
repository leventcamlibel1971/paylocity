using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Interfaces
{
    public interface IPayrollOperationHandler<in TRequest, out TResponse>
    {
        /// <summary>
        ///     Defines generic payroll operation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="payrollConfig"> Employee Payroll Config</param>
        /// <returns></returns>
        TResponse Process(TRequest request, PayrollConfigModel payrollConfig);

        /// <summary>
        ///     sets yp discount strategy
        /// </summary>
        /// <param name="discountStrategy"></param>
        void SetDiscountStrategy(IDiscountStrategy discountStrategy);
    }
}