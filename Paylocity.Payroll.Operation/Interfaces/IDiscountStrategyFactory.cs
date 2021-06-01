using Paylocity.Payroll.Operation.Models.Enums;

namespace Paylocity.Payroll.Operation.Interfaces
{
    public interface IDiscountStrategyFactory
    {
        /// <summary>
        /// </summary>
        /// <param name="discountType">Discount Type: default is none</param>
        /// <returns></returns>
        IDiscountStrategy Create(DiscountTypeEnum discountType = DiscountTypeEnum.None);
    }
}