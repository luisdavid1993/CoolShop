using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerInterface
{
    public interface IDiscount
    {
        decimal Calculate(ICustomer obj);
    }
}
