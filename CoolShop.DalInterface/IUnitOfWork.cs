using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.DalInterface
{
    //Desing Pattern =-  Unit of Work Pattern, (Cordinate the work of multiples repositories)
    /// <summary>
    /// wherever is the repository concrete method, It is 
    /// </summary>
    // goin to use this centralize methods 
    public interface IUnitOfWork
    {
        void Committ();
        void RollBack();
    }
}
