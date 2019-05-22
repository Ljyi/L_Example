using DAL.DBContext.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBBase
{
    public class DbContextFactory
    {
        public static DBEntity GetCurrentContext(DataBaseType dataBaseType= DataBaseType.Sql)
        {
            DBEntity _nContext = CallContext.GetData("ERPContext") as DBEntity;
            switch (dataBaseType)
            {
                case DataBaseType.Sql:
                    if (_nContext == null)
                    {
                        _nContext = new DBEntity();
                        CallContext.SetData("ERPContext", _nContext);
                    }
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.Redis:
                    break;
                default:
                    break;
            }
            return _nContext;
        }

    }
}
