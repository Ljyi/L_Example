using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Table("Role")]
    public class Role : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string RoleName { get; set; }

        [MaxLength(100)]
        [Required]
        public string Status { get; set; }
    }
}
