using FluentAssertions;
using Paylocity.Payroll.Operation.Factories;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models.Enums;
using Paylocity.Payroll.Operation.Strategies;
using Xunit;

namespace Paylocity.Payroll.Tests.UnitTests
{
    public class DiscountStrategyFactoryUnitTests
    {
        [Fact]
        public void It_Should_Implement_IDiscountStrategyFactory()
        {
            var discountStrategyFactory = new DiscountStrategyFactory();
            discountStrategyFactory.Should().BeAssignableTo<IDiscountStrategyFactory>();
        }

        [Fact]
        public void It_Should_Return_NameDiscountStrategy()
        {
            var discountStrategyFactory = new DiscountStrategyFactory();
            var discountStrategy = discountStrategyFactory.Create(DiscountTypeEnum.Name);
            discountStrategy.Should().BeAssignableTo<NameDiscountStrategy>();
        }

        [Fact]
        public void It_Should_Return_NoneDiscountStrategy()
        {
            var discountStrategyFactory = new DiscountStrategyFactory();
            var discountStrategy = discountStrategyFactory.Create();
            discountStrategy.Should().BeAssignableTo<NoneDiscountStrategy>();
        }
    }
}