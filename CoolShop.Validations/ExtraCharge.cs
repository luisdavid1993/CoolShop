using CoolShop.CustomerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerHelpper
{
    public class ExtraChargeSatSun : IExtraCharge
    {
        /// <summary>
        /// discount on days Sunday or Saturday
        /// </summary>
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillDate.DayOfWeek == DayOfWeek.Saturday || obj.BillDate.DayOfWeek == DayOfWeek.Sunday)
                return obj.BillAmount * Convert.ToDecimal(0.01);
            else
                return 0;
        }
    }

    public class ExtraChargeNotAvalible : IExtraCharge // NULL pattern (when it do not need a vale, a defalut value)
    {
        public decimal Calculate(ICustomer obj)
        {
            return 0;
        }
    }
}
