using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWork
{
    /// <summary>
    /// 工作单元实现
    /// </summary>
    public class UnitOfWorks : IUnitOfWork.UnitOfWork.IUnitOfWork
    {
        /// <summary>
        /// 注入对象
        /// </summary>
        private IUnitOfWork.UnitOfWork.IUnitOfWorkContext context;

        /// <summary>
        /// 维护一个Sql语句的命令列表
        /// </summary>
        private List<CommandObject> commands;

        public UnitOfWorks(IUnitOfWork.UnitOfWork.IUnitOfWorkContext context)
        {
            commands = new List<CommandObject>();
            this.context = context;
        }

        /// <summary>
        /// 增、删、改命令 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Command(string commandText, IDictionary<string, object> parameters)
        {
            IsCommited = false;
            commands.Add(new CommandObject(commandText, parameters));
            return 1;
        }

        /// <summary>
        /// 提交状态
        /// </summary>
        public bool IsCommited { get; set; }

        /// <summary>
        /// 提交方法
        /// </summary>
        /// <returns></returns>
        public void Commit()
        {
            if (IsCommited) { return; }
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var command in commands)
                {
                    context.ExecuteNonQuery(command.command, command.parameters);
                }
                scope.Complete();
                IsCommited = true;
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBack()
        {
            IsCommited = false;
        }
    }
}
