using CoolShop.CustomerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerHelpper
{
    /// <summary>
    /// Discount whent the bill amount is greater than 10.000
    /// </summary>
    public class DiscountBillAmount : IDiscount
    {
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillAmount > 10000)
                return obj.BillAmount * Convert.ToDecimal(0.02);
            else
                return obj.BillAmount * Convert.ToDecimal(0.01);
        }
    }

    /// <summary>
    /// discount on days Sunday or Saturday
    /// </summary>
    public class DiscountSatSun: IDiscount
    {
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillDate.DayOfWeek == DayOfWeek.Saturday || obj.BillDate.DayOfWeek == DayOfWeek.Sunday)
                return obj.BillAmount * Convert.ToDecimal(0.01);
            else
                return obj.BillAmount * Convert.ToDecimal(0.005);
        }
    }

    public class DiscountNotAvalible : IDiscount  //Design pattern :- NULL pattern (when it do not need a vale, a defalut value)
    {
        public decimal Calculate(ICustomer obj)
        {
            return 0;
        }
    }

}
