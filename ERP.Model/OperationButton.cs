using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Table("OperationButton")]
    public class OperationButton : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string ButtonName { get; set; }
        [MaxLength(100)]
        [Required]
        public string ButtonCode { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
        [MaxLength(200)]
        public string InputType { get; set; }
        [MaxLength(200)]
        public string Style { get; set; }
        [MaxLength(200)]
        public string Remark { get; set; }


    }
}
