﻿using Paylocity.Payroll.Operation.Enums;

namespace Paylocity.Payroll.Operation.Models
{
    public class DiscountModel
    {
        public decimal DiscountRate { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }
    }
}