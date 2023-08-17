using CoolShop.CustomerInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolShop.FactoryCustomer;
using System.Configuration;
using CoolShop.CustomerFactory;

namespace CoolShop.AdoDAL
{
    public class CustommerDAL : TemplateADO<CustomerBase>
    {
        public override void Add(CustomerBase obj)
        {
            string erroMessage = string.Empty;
            if (obj.Validete(out erroMessage))
                base.Add(obj);
            else
                throw new Exception($"Errors:{Environment.NewLine} {erroMessage}");
        }
        protected override void ExecuteCommand(CustomerBase obj)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[InsertClient]";
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmClientType", SqlDbType = SqlDbType.VarChar, Value = obj.CustomerType });
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmCustomerName", SqlDbType = SqlDbType.VarChar, Value = obj.CustomerName });
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmPhoneNumber", SqlDbType = SqlDbType.VarChar, Value = obj.PhoneNumber });
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmBillAmount", SqlDbType = SqlDbType.Decimal, Value = obj.BillAmount });
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmBillDate", SqlDbType = SqlDbType.Date, Value = obj.BillDate });
            command.Parameters.Add(new SqlParameter { ParameterName = "@prmAddress", SqlDbType = SqlDbType.VarChar, Value = obj.Address });
            command.ExecuteNonQuery();
        }

        protected override List<CustomerBase> ExecuteCommand()
        {
            List<CustomerBase> customers = new List<CustomerBase>();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[SelectClient]";
            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    string cusType = dr["CustomerType"].ToString();
                    CustomerBase customer = FactoryLookUp.Create(cusType);
                    customer.Id = Convert.ToInt32(dr["Id"]);
                    customer.CustomerType = cusType;
                    customer.CustomerName = dr["CustomerName"].ToString();
                    customer.PhoneNumber = dr["PhoneNumber"].ToString();
                    customer.BillAmount = dr["BillAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["BillAmount"]);
                    customer.BillDate = Convert.ToDateTime(dr["BillDate"]);
                    customer.Address = dr["Address"].ToString();
                    customers.Add(customer);
                }
            }
            return customers;
        }

        #region Normal method examples 
        private void NormalSqlInsertMethodExample(CustomerBase obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CoolShopConn"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[InsertClient]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmClientType", SqlDbType = SqlDbType.VarChar, Value = obj.CustomerType });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmCustomerName", SqlDbType = SqlDbType.VarChar, Value = obj.CustomerName });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmPhoneNumber", SqlDbType = SqlDbType.VarChar, Value = obj.PhoneNumber });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmBillAmount", SqlDbType = SqlDbType.Decimal, Value = obj.BillAmount });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmBillDate", SqlDbType = SqlDbType.Date, Value = obj.BillDate });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@prmAddress", SqlDbType = SqlDbType.VarChar, Value = obj.Address });
                    command.ExecuteNonQuery();
                }
            }
        }

        private void NormalSqlSelectMethodExample()
        {
            List<CustomerBase> customers = new List<CustomerBase>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CoolShopConn"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SelectClient]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.HasRows)
                            {
                                string cusType = reader["CustomerType"].ToString();
                                CustomerBase customer = FactoryLookUp.Create(cusType);
                                customer.Id = Convert.ToInt32(reader["Id"]);
                                customer.CustomerType = cusType;
                                customer.CustomerName = reader["CustomerName"].ToString();
                                customer.PhoneNumber = reader["PhoneNumber"].ToString();
                                customer.BillAmount = reader["BillAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BillAmount"]);
                                customer.BillDate = Convert.ToDateTime(reader["BillDate"]);
                                customer.Address = reader["Address"].ToString();
                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
