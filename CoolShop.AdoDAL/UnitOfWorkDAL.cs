using CoolShop.DalInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.AdoDAL
{
    public class UnitOfWorkDAL : IUnitOfWork
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public UnitOfWorkDAL()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoolShopConn"].ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }
        public void Committ()
        {
            Transaction.Commit();
            Connection.Close();
        }

        public void RollBack()
        {
            Transaction.Dispose();
            Connection.Close();
        }
    }
}
