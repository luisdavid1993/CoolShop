using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerInterface
{
    public interface IValidation<AnyType> where AnyType : class
    {
        //Desing Pattern =- Strategy help to (choose algorithm dynamically)
        bool Validate(AnyType anyType, out string errorMessage);
    }
}
