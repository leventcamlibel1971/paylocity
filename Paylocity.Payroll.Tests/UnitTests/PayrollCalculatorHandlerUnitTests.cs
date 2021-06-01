using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;
using Paylocity.Payroll.Operation.Models.Enums;
using Paylocity.Payroll.Operation.Repositories;
using Paylocity.Payroll.Operation.Strategies;
using Xunit;

namespace Paylocity.Payroll.Tests.UnitTests
{
    public class PayrollCalculatorHandlerUnitTests
    {
        private readonly IPayrollConfigRepository _payrollConfigRepository;

        public PayrollCalculatorHandlerUnitTests()
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
        public void It_Should_Implement_IPayrollOperationHandler()
        {
            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.Should().BeAssignableTo<IPayrollOperationHandler<EmployeeModel, PaycheckModel>>();
        }

        [Fact]
        public void It_Should_Throw_Exception_When_DiscountStrategy_IsNot_Set()
        {
            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            var ex = Assert.Throws<Exception>(() =>
                payrollCalculatorHandler.Process(new EmployeeModel
                {
                    Name = "Joe Doe"
                }, default));

            ex.Message.Should().Be("Discount strategy is not provided.");
        }

        [Fact]
        public void It_Should_Not_Return_Discount_For_NonQualified_Employee()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();
            const decimal expectedDeductible = 38.46m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Joe Doe"
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }

        [Fact]
        public void It_Should_Return_DiscountFor_Qualified_Employee()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            const decimal expectedDeductible = 34.62m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Al Doe"
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }

        [Fact]
        public void It_Should_Not_Return_Discount_For_NonQualified_Dependent()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            const decimal expectedDeductible = 57.69m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Joe Doe",
                Dependents = new List<PersonModel>
                {
                    new() {Name = "Jane Doe"}
                }
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }

        [Fact]
        public void It_Should_Return_DiscountFor_Qualified_Dependent()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            const decimal expectedDeductible = 55.77m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Joe Doe",
                Dependents = new List<PersonModel>
                {
                    new() {Name = "Ashley Doe"}
                }
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }

        [Fact]
        public void It_Should_Return_DiscountFor_Qualified_Dependent_And_Not_Return_NonQualified()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            const decimal expectedDeductible = 75.00m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Joe Doe",
                Dependents = new List<PersonModel>
                {
                    new() {Name = "Ashley Doe"},
                    new() {Name = "Jane Doe"}
                }
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }

        [Fact]
        public void It_Should_Return_DiscountFor_Qualified_Employee_And_Dependent()
        {
            var payrollConfig = _payrollConfigRepository.GetPayrollConfig();

            const decimal expectedDeductible = 51.93m;

            var payrollCalculatorHandler = new PayrollCalculatorHandler();
            payrollCalculatorHandler.SetDiscountStrategy(new NameDiscountStrategy());

            var employee = new EmployeeModel
            {
                Name = "Al Doe",
                Dependents = new List<PersonModel>
                {
                    new() {Name = "Ashley Doe"}
                }
            };
            var paycheck = payrollCalculatorHandler.Process(employee, payrollConfig);

            paycheck.TotalDeductible.Should().Be(expectedDeductible);
            paycheck.NetAmount.Should().Be(paycheck.GrossAmount - expectedDeductible);
        }
    }
}