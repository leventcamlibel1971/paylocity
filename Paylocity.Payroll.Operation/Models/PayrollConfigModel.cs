namespace Paylocity.Payroll.Operation.Models
{
    public class PayrollConfigModel
    {
        public decimal EmployeeAnnualCostOfBenefit { get; set; }
        public decimal DependentAnnualCostOfBenefit { get; set; }
        public int NumberOfPaychecksInAYear { get; set; }
        public decimal PaycheckAmount { get; set; }
        public DiscountModel Discount { get; set; }
    }
}