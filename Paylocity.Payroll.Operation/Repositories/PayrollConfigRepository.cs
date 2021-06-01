using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;
using Paylocity.Payroll.Operation.Models.Enums;

namespace Paylocity.Payroll.Operation.Repositories
{
    public class PayrollConfigRepository : IPayrollConfigRepository
    {
        public PayrollConfigModel GetPayrollConfig()
        {
            //return sample data
            var response = new PayrollConfigModel
            {
                DependentAnnualCostOfBenefit = 500m,
                EmployeeAnnualCostOfBenefit = 1000m,
                PaycheckAmount = 2000m,
                NumberOfPaychecksInAYear = 26,
                Discount = new DiscountModel
                {
                    DiscountType = DiscountTypeEnum.Name,
                    DiscountRate = 0.10m
                }
            };

            return response;
        }
    }
}