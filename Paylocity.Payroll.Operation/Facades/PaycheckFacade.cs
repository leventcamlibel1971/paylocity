using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.Operation.Facades
{
    public class PaycheckFacade : IPaycheckFacade
    {
        private readonly IDiscountStrategyFactory _discountStrategyFactory;
        private readonly IPayrollOperationHandler<EmployeeModel, PaycheckModel> _payrollCalculatorHandler;
        private readonly IPayrollConfigRepository _payrollConfigRepository;

        public PaycheckFacade(IPayrollOperationHandler<EmployeeModel, PaycheckModel> payrollCalculatorHandler,
            IDiscountStrategyFactory discountStrategyFactory,
            IPayrollConfigRepository payrollConfigRepository)
        {
            _payrollCalculatorHandler = payrollCalculatorHandler;
            _discountStrategyFactory = discountStrategyFactory;
            _payrollConfigRepository = payrollConfigRepository;
        }

        public PaycheckModel Calculate(EmployeeModel employeeModel)
        {
            //get payroll config
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            //figure out the discount strategy
            var discountStrategy = _discountStrategyFactory.Create(payrollConfig.Discount.DiscountType);

            //set the discount strategy
            _payrollCalculatorHandler.SetDiscountStrategy(discountStrategy);

            //calculate the paycheck
            var payCheck = _payrollCalculatorHandler.Process(employeeModel, payrollConfig);

            return payCheck;
        }
    }
}