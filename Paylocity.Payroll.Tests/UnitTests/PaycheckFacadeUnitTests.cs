using FluentAssertions;
using Moq;
using Paylocity.Payroll.Operation.Enums;
using Paylocity.Payroll.Operation.Facades;
using Paylocity.Payroll.Operation.Factories;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;
using Paylocity.Payroll.Operation.Repositories;
using Xunit;

namespace Paylocity.Payroll.Tests.UnitTests
{
    public class PaycheckFacadeUnitTests
    {
        private readonly IPayrollConfigRepository _payrollConfigRepository;

        public PaycheckFacadeUnitTests()
        {
            var payrollConfigRepositoryMock = new Mock<IPayrollConfigRepository>();
            payrollConfigRepositoryMock.Setup(x => x.GetPayrollConfig()).Returns(new PayrollConfigModel
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
            });

            _payrollConfigRepository = payrollConfigRepositoryMock.Object;
        }

        [Fact]
        public void It_Should_Implement_IPaycheckFacade()
        {
            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            var discountStrategyFactory = new DiscountStrategyFactory();
            var paycheckFacade = new PaycheckFacade(payrollCalculatorHandler, discountStrategyFactory,
                _payrollConfigRepository);
            paycheckFacade.Should().BeAssignableTo<IPaycheckFacade>();
        }

        //more unit tests can be added. the units will be the same as in  PayrollCalculatorHandlerUnitTests

        [Fact]
        public void It_Should_Not_Return_Discount_For_NonQualified_Employee()
        {
            const decimal expectedDeductible = 38.46m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            var discountStrategyFactory = new DiscountStrategyFactory();
            var paycheckFacade = new PaycheckFacade(payrollCalculatorHandler, discountStrategyFactory,
                _payrollConfigRepository);

            var employee = new EmployeeModel
            {
                Name = "Joe Doe"
            };
            var paycheck = paycheckFacade.Calculate(employee);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }
    }
}