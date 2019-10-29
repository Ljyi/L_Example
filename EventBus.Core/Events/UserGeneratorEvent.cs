using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Events
{
    /// <summary>
    /// 模型对象
    /// </summary>
    public class UserGeneratorEvent : IEvent
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
