using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Table("User")]
    public class User : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LogingName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
