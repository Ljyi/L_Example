using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class SysMenu : BaseModel
    {
        /// <summary>
        /// 主键ID（自增）
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string MenuCode { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单级别
        /// </summary>
        [Required]
        public int MenuLevel { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        [Required]
        public int ParentID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNumber { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
