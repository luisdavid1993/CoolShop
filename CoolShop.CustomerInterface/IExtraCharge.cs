using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerInterface
{
    public interface IExtraCharge
    {
        decimal Calculate(ICustomer obj);
    }
}
