using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUnitOfWork.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Command(string commandText, IDictionary<string, object> parameters);

        /// <summary>
        /// 事务的提交状态
        /// </summary>
        bool IsCommited { get; set; }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBack();
    }
}
