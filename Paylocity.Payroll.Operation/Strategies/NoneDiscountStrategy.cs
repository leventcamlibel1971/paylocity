using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Strategies
{
    public class NoneDiscountStrategy : IDiscountStrategy
    {
        public decimal Apply(PayrollConfigModel payrollConfig, EmployeeModel employee)
        {
            var totalDeductible = CalculateCostOfBenefit(payrollConfig.EmployeeAnnualCostOfBenefit,
                payrollConfig.NumberOfPaychecksInAYear);

            foreach (var dependent in employee.Dependents)
                totalDeductible += CalculateCostOfBenefit(payrollConfig.DependentAnnualCostOfBenefit,
                    payrollConfig.NumberOfPaychecksInAYear);

            return totalDeductible;
        }

        private static decimal CalculateCostOfBenefit(decimal annualCostOfBenefit, int numberOfPaychecksInAYear)
        {
            var costOfBenefit = annualCostOfBenefit / numberOfPaychecksInAYear;
            return decimal.Round(costOfBenefit, 2);
        }
    }
}