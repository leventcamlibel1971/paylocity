using System;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Repositories
{
    public class PayrollCalculatorHandler : IPayrollOperationHandler<EmployeeModel, PaycheckModel>
    {
        private IDiscountStrategy _discountStrategy;

        public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public PaycheckModel Process(EmployeeModel employee, PayrollConfigModel payrollConfig)
        {
            if (_discountStrategy == null)
                throw new Exception("Discount strategy is not provided.");

            var totalDeductible = _discountStrategy.Apply(payrollConfig, employee);

            return new PaycheckModel
            {
                GrossAmount = payrollConfig.PaycheckAmount,
                TotalDeductible = totalDeductible,
                NetAmount = payrollConfig.PaycheckAmount - totalDeductible
            };
        }
    }
}