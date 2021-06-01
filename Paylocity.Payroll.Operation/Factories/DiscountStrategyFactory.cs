using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models.Enums;
using Paylocity.Payroll.Operation.Strategies;

namespace Paylocity.Payroll.Operation.Factories
{
    public class DiscountStrategyFactory : IDiscountStrategyFactory
    {
        public IDiscountStrategy Create(DiscountTypeEnum discountType = DiscountTypeEnum.None)
        {
            return discountType switch
            {
                DiscountTypeEnum.Name => new NameDiscountStrategy(),
                DiscountTypeEnum.None => new NoneDiscountStrategy(),
                _ => new NoneDiscountStrategy()
            };
        }
    }
}