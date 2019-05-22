using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Table("UserRights")]
    public class UserRights : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        [Required]
        public int UserRoleId { get; set; }
        [Required]
        public int OperationButtonId { get; set; }
        [Required]
        public int SysMenuId { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual SysMenu SysMenu { get; set; }
        public virtual OperationButton OperationButton { get; set; }
    }
}
