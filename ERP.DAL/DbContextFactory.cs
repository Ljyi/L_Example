using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL
{
    public class DbContextFactory
    {
        public static BaseDbContext GetCurrentContext(DataBaseType dataBaseType = DataBaseType.Sql)
        {
            BaseDbContext baseDbContext = CallContext.GetData("ERPContext") as BaseDbContext;
            switch (dataBaseType)
            {
                case DataBaseType.Sql:
                    if (baseDbContext == null)
                    {
                        baseDbContext = new BaseDbContext();
                        CallContext.SetData("ERPContext", baseDbContext);
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
            return baseDbContext;
        }
    }
}
