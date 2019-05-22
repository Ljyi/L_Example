using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Table("UserRole")]
    public class UserRole : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
