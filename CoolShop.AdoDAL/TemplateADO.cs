using CoolShop.DalInterface;
using System.Configuration;
using System.Data.SqlClient;

namespace CoolShop.AdoDAL
{
    public abstract class TemplateADO<AnyType> : CommonDAL<AnyType>
    {
        protected SqlConnection sqlConnection;
        protected SqlCommand command;
        IUnitOfWork unitOfWork;

        public override void SetUnitOfWork(IUnitOfWork uow)
        {
            unitOfWork = uow;
            sqlConnection = ((UnitOfWorkDAL)uow).Connection;
            command = new SqlCommand();
            command.Connection = sqlConnection;
            command.Transaction = ((UnitOfWorkDAL)uow).Transaction;
        }
        private void Open()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoolShopConn"].ConnectionString);
                sqlConnection.Open();
                command = new SqlCommand();
                command.Connection = sqlConnection;
            }

        }
        protected abstract void ExecuteCommand(AnyType obj);
        protected abstract List<AnyType> ExecuteCommand(); // Child classes 
        private void Close()
        {
            if (unitOfWork == null)
            {
                sqlConnection.Close();
                sqlConnection = null;
            }
        }
        private void Execute(AnyType obj)  //Desing Pattern =- Temnplate Pattern (fix list of steps)
        {
            Open();
            ExecuteCommand(obj);
            Close();
        }

        public List<AnyType> Execute() // Fixed Sequence select
        {
            List<AnyType> objTypes;
            Open();
            objTypes = ExecuteCommand();
            Close();
            return objTypes;
        }
        public override void Save()
        {
            foreach (AnyType item in _items)
            {
                Execute(item);
            }
        }

        public override List<AnyType> Search()
        {
            sqlConnection = null;
            unitOfWork = null;
            return Execute();
        }
    }
}