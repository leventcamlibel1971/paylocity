namespace Paylocity.Payroll.Operation.Models
{
    public class PaycheckModel
    {
        public decimal GrossAmount { get; set; }
        public decimal TotalDeductible { get; set; }
        public decimal NetAmount { get; set; }
    }
}