using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    namespace DAL.DBContext
    {
        public class DBEntity : DbContext
        {
            public DBEntity()
                : base("name=SupplyCoordinationEntities")
            {
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

            }
            public virtual DbSet<SendSupplyMail> SendSupplyMail { get; set; }
            public virtual DbSet<SupplyAccount> SupplyAccount { get; set; }
            public virtual DbSet<SupplyAction> SupplyAction { get; set; }
            public virtual DbSet<SupplyCategory> SupplyCategory { get; set; }
            public virtual DbSet<SupplyLoginLog> SupplyLoginLog { get; set; }
            public virtual DbSet<SupplyMaiMatch> SupplyMaiMatch { get; set; }
            public virtual DbSet<SupplyModel> SupplyModel { get; set; }
            public virtual DbSet<SupplyModelAction> SupplyModelAction { get; set; }
            public virtual DbSet<SupplyOperation> SupplyOperation { get; set; }
            public virtual DbSet<SupplyOperationDetail> SupplyOperationDetail { get; set; }
            public virtual DbSet<SupplyPostData> SupplyPostData { get; set; }

        }
    }

}
