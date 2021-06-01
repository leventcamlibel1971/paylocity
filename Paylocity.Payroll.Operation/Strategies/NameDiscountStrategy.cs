using System;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Strategies
{
    public class NameDiscountStrategy : IDiscountStrategy
    {
        public decimal Apply(PayrollConfigModel payrollConfig, EmployeeModel employee)
        {
            var totalDeductible = CalculateCostOfBenefit(employee.Name, payrollConfig.EmployeeAnnualCostOfBenefit,
                payrollConfig.NumberOfPaychecksInAYear, payrollConfig.Discount.DiscountRate);

            foreach (var dependent in employee.Dependents)
                totalDeductible += CalculateCostOfBenefit(dependent.Name,
                    payrollConfig.DependentAnnualCostOfBenefit,
                    payrollConfig.NumberOfPaychecksInAYear, payrollConfig.Discount.DiscountRate);

            return totalDeductible;
        }

        private static decimal CalculateCostOfBenefit(string name, decimal annualCostOfBenefit,
            int numberOfPaychecksInAYear, decimal discountRate)
        {
            var discountEligible = name.TrimStart().StartsWith("A", StringComparison.OrdinalIgnoreCase);

            var costOfBenefit = annualCostOfBenefit / numberOfPaychecksInAYear;


            var paycheckCostOfBenefit = discountEligible
                ? costOfBenefit * (1 - discountRate)
                : costOfBenefit;


            return decimal.Round(paycheckCostOfBenefit, 2);
        }
    }
}